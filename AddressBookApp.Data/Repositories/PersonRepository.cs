using AddressBookApp.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using AddressBookApp.Data.Context;
using System.Data.Entity;
using AddressBookApp.Common.Utils.Models;

namespace AddressBookApp.Data.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly AddressBookDbContext _dbContext;
        public PersonRepository()
        {
            _dbContext = new AddressBookDbContext();
        }

        public void CreateNewPerson(Person person)
        {
            _dbContext.People.Add(person);
            _dbContext.SaveChanges();
        }

        public void DeletePerson(int personId)
        {
            var targetPerson = _dbContext.People.SingleOrDefault(x => x.Id == personId);
            _dbContext.People.Remove(targetPerson);
            _dbContext.SaveChanges();
        }

        public Person GetById(int id)
        {
            return _dbContext.People.Include(x => x.Emails)
                                    .Include(x => x.Addresses)
                                    .SingleOrDefault(x => x.Id == id);
        }

        public IEnumerable<Person> GetAll()
        {
            return _dbContext.People.Include(x => x.Emails)
                                    .Include(x => x.Addresses)
                                    .ToList();
        }

        public void UpdatePerson(Person personUpdatesModel)
        {
            var personBeingUpdated = _dbContext.People.SingleOrDefault(x => x.Id == personUpdatesModel.Id);
            personBeingUpdated.Name = personUpdatesModel.Name;
            personBeingUpdated.Surname = personUpdatesModel.Surname;
            _dbContext.SaveChanges();
        }

        public IEnumerable<Person> GetFilteredList(DataTablesFilteringModel filter,
                                                   out int filteredCount,
                                                   out int totalCount)
        {
            var contactsQuery = _dbContext.People
                                          .Include(x => x.Emails)
                                          .Include(x => x.Addresses)
                                          .AsQueryable();

            totalCount = contactsQuery.Count();
            filteredCount = 0;

            //Filter by global search value
            contactsQuery = FilterBySearchCriteria(contactsQuery, filter.SearchConfig);

            filteredCount = contactsQuery.Count();
            contactsQuery = contactsQuery.OrderBy(filter.SortingConfig == String.Empty ? "surname asc" : filter.SortingConfig);

            //Paginate
            contactsQuery = contactsQuery.Skip(filter.StartIndex)
                                         .Take(filter.ItemAmount);

            return contactsQuery.ToList();
        }

        private IQueryable<Person> FilterBySearchCriteria(IQueryable<Person> query, Dictionary<string, string> searchCriteria)
        {
            string searchValue = string.Empty;
            if (searchCriteria["GlobalSearch"] != String.Empty)
            {
                searchValue = searchCriteria["GlobalSearch"].Trim();
                query = query.Where(x => x.Name.Contains(searchValue) ||
                                                    x.Surname.Contains(searchValue) ||
                                                    x.Emails.Any(email => email.Name.Contains(searchValue)) ||
                                                    x.Addresses.Any(addr => addr.Name.Contains(searchValue)));
            }

            if (searchCriteria["Name"] != String.Empty)
            {
                searchValue = searchCriteria["Name"].Trim();
                query = query.Where(x => x.Name.Contains(searchValue));
            }

            if (searchCriteria["Surname"] != String.Empty)
            {
                searchValue = searchCriteria["Surname"].Trim();
                query = query.Where(x => x.Surname.Contains(searchValue));
            }

            if (searchCriteria["Emails"] != String.Empty)
            {
                searchValue = searchCriteria["Emails"].Trim();
                query = query.Where(x => x.Emails.Any(email => email.Name.Contains(searchValue)));
            }

            if (searchCriteria["Addresses"] != String.Empty)
            {
                searchValue = searchCriteria["Addresses"].Trim();
                query = query.Where(x => x.Addresses.Any(address => address.Name.Contains(searchValue)));
            }

            return query;
        }
    }
}

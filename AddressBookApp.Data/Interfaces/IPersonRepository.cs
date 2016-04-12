using AddressBookApp.Data.Context;
using AddressBookApp.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBookApp.Data.Interfaces
{
    public interface IPersonRepository
    {
        IEnumerable<Person> GetAll();
        IEnumerable<Person> GetFilteredList(DataTablesFilteringModel filteringModel, out int filteredCount, out int totalCount);
        Person GetById(int id);
        void CreateNewPerson(Person person);
        void DeletePerson(int id);
        void UpdatePerson(Person person);
    }
}

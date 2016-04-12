using AddressBookApp.Common.Utils.Models;
using AddressBookApp.Data.Context;
using System.Collections.Generic;

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

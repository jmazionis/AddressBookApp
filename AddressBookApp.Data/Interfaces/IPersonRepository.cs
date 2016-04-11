using AddressBookApp.Data.Context;
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
        IEnumerable<Person> GetFilteredList(string searchValue, int itemsToTake, int itemsStartIndex, string sortingConfig, out int filteredCount, out int totalCount);
        Person GetById(int id);
        void CreateNewPerson(Person person);
        void DeletePerson(int id);
        void UpdatePerson(Person person);
    }
}

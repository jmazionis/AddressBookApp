using AddressBookApp.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBookApp.Data.Interfaces
{
    public interface IAddressRepository
    {
        Address GetById(int id);
        void AddNewAddressToContact(Address address);
        void DeleteAddress(int id);
        void UpdateAddress(Address address);        
    }
}

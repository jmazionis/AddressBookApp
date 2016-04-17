using AddressBookApp.Data.Context;
using AddressBookApp.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBookApp.Data.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly IAddressBookDbContext _dbContext;
        public AddressRepository(IAddressBookDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddNewAddressToContact(Address address)
        {
            _dbContext.Addresses.Add(address);            
        }       

        public void DeleteAddress(int addressId)
        {
            var targetAddress = _dbContext.Addresses.SingleOrDefault(x => x.Id == addressId);
            _dbContext.Addresses.Remove(targetAddress);            
        }

        public Address GetById(int id)
        {
            return _dbContext.Addresses.SingleOrDefault(x => x.Id == id);
        }

        public void UpdateAddress(Address addressModel)
        {
            _dbContext.Entry(addressModel).State = EntityState.Modified;
            addressModel.Name = addressModel.Name;            
        }        
    }
}

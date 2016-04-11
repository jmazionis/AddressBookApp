using AddressBookApp.Data.Context;
using AddressBookApp.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBookApp.Data.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly AddressBookDbContext _dbContext;
        public AddressRepository()
        {
            _dbContext = new AddressBookDbContext();
        }

        public void AddNewAddressToContact(Address address)
        {
            _dbContext.Addresses.Add(address);
            _dbContext.SaveChanges();
        }       

        public void DeleteAddress(int addressId)
        {
            var targetAddress = _dbContext.Addresses.SingleOrDefault(x => x.Id == addressId);
            _dbContext.Addresses.Remove(targetAddress);
            _dbContext.SaveChanges();
        }

        public Address GetById(int id)
        {
            return _dbContext.Addresses.SingleOrDefault(x => x.Id == id);
        }

        public void UpdateAddress(Address addressModel)
        {
            var targetEmail = _dbContext.Addresses.SingleOrDefault(x => x.Id == addressModel.Id);
            targetEmail.Name = addressModel.Name;
            _dbContext.SaveChanges();
        }        
    }
}

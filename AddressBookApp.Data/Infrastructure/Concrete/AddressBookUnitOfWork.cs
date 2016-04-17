using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddressBookApp.Data.Interfaces;
using AddressBookApp.Data.Context;

namespace AddressBookApp.Data.Infrastructure.Concrete
{
    public class AddressBookUnitOfWork : IAddressBookUnitOfWork
    {
        private readonly IAddressBookDbContext _dbContext;
        private readonly IEmailRepository _emailRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IPersonRepository _personRepository;        

        public AddressBookUnitOfWork(IAddressBookDbContext dbContext,
                                     IEmailRepository emailRepository,
                                     IAddressRepository addressRepository,
                                     IPersonRepository personRepository)
        {
            _dbContext = dbContext;
            _emailRepository = emailRepository;
            _addressRepository = addressRepository;
            _personRepository = personRepository;
        }

        public IAddressRepository AddressRepository
        {
            get
            {
                return _addressRepository;
            }
        }

        public IEmailRepository EmailRepository
        {
            get
            {
                return _emailRepository;
            }
        }

        public IPersonRepository PersonRepository
        {
            get
            {
                return _personRepository;
            }
        }

        public int CommitChanges()
        {
            return _dbContext.SaveChanges();
        }       
    }
}

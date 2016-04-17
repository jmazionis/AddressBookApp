using AddressBookApp.Data.Context;
using AddressBookApp.Data.Interfaces;
using System.Data.Entity;
using System.Linq;

namespace AddressBookApp.Data.Repositories
{
    public class EmailRepository : IEmailRepository
    {
        private readonly IAddressBookDbContext _dbContext;

        public EmailRepository(IAddressBookDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void AddNewEmailToContact(Email email)
        {
            _dbContext.Emails.Add(email);            
        }

        public void DeleteEmail(int id)
        {
            var targetEmail = _dbContext.Emails.SingleOrDefault(x => x.Id == id);
            _dbContext.Emails.Remove(targetEmail);            
        }

        public void UpdateEmail(Email email)
        {            
            _dbContext.Entry(email).State = EntityState.Modified;
            email.Name = email.Name;              
        }

        public Email GetById(int id)
        {
            return _dbContext.Emails.SingleOrDefault(x => x.Id == id);
        }
    }
}

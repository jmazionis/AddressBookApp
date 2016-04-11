using AddressBookApp.Data.Context;
using AddressBookApp.Data.Interfaces;
using System.Linq;

namespace AddressBookApp.Data.Repositories
{
    public class EmailRepository : IEmailRepository
    {
        private readonly AddressBookDbContext _dbContext;

        public EmailRepository()
        {
            _dbContext = new AddressBookDbContext();
        }
        public void AddNewEmailToContact(Email email)
        {
            _dbContext.Emails.Add(email);
            _dbContext.SaveChanges();
        }

        public void DeleteEmail(int id)
        {
            var targetEmail = _dbContext.Emails.SingleOrDefault(x => x.Id == id);
            _dbContext.Emails.Remove(targetEmail);
            _dbContext.SaveChanges();
        }

        public void UpdateEmail(Email email)
        {
            var targetEmail = _dbContext.Emails.SingleOrDefault(x => x.Id == email.Id);
            targetEmail.Name = targetEmail.Name;
            _dbContext.SaveChanges();
        }

        public Email GetById(int id)
        {
            return _dbContext.Emails.SingleOrDefault(x => x.Id == id);
        }
    }
}

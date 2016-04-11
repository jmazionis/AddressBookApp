using AddressBookApp.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBookApp.Data.Interfaces
{
    public interface IEmailRepository
    {
        Email GetById(int id);
        void AddNewEmailToContact(Email email);
        void DeleteEmail(int id);
        void UpdateEmail(Email email);
    }
}

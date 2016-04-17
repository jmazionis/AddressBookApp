using AddressBookApp.Data.Interfaces;
using System;

namespace AddressBookApp.Data.Infrastructure
{
    public interface IAddressBookUnitOfWork
    {
        IAddressRepository AddressRepository { get; }
        IEmailRepository EmailRepository { get; }
        IPersonRepository PersonRepository { get; }
        int CommitChanges();
    }
}

using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AddressBookApp.ViewModels
{
    public class ContactListItemViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Surname { get; set; }        
        public IEnumerable<EmailViewModel> Emails { get; set; }
        public IEnumerable<AddressViewModel> Addresses { get; set; }
    }
}

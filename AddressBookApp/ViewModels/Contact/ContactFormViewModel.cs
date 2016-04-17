using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBookApp.ViewModels
{
    public class ContactFormViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "You must provide contact's name")]
        public string Name { get; set; }       
        [Required(ErrorMessage = "You must provide contact's surname")]
        public string Surname { get; set; }               
        public List<EmailViewModel> Emails { get; set; }
        public List<AddressViewModel> Addresses { get; set; }
    }
}

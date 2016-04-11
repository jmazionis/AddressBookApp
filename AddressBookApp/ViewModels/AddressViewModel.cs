using System.ComponentModel.DataAnnotations;

namespace AddressBookApp.ViewModels
{
    public class AddressViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "You must provide address")]        
        public string Name { get; set; }
        public int PersonId { get; set; }
    }
}

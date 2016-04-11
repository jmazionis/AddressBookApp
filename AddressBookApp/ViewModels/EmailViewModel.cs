using System.ComponentModel.DataAnnotations;

namespace AddressBookApp.ViewModels
{
    public class EmailViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "You must provide email")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Name { get; set; }
        public int PersonId { get; set; }
    }
}

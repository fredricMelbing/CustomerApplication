using System.ComponentModel.DataAnnotations;

namespace CustomerApplication.Domain.DTO
{
    public class CustomerCreateDTO
    {
        [Required(ErrorMessage = "Name are required")]
        public string FullName { get; set; }
        public string Title { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }        
        public string Address { get; set; }        
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Salescontact is required")]
        public int SalesId { get; set; }
    }
}

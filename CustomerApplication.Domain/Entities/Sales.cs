using System.ComponentModel.DataAnnotations;

namespace CustomerApplication.Domain.Entities
{
    public class Sales
    {
        [Key]
        [Required]
        public string SalesId { get; set; }
        [Required(ErrorMessage = "Name are required")]
        public string FullName { get; set; }
        [Phone]
        [Required(ErrorMessage = "Mobile no. is required")]
        public string Phonenumber { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "Email. is required")]        
        public string Email { get; set; }
        public List<Customer>? Customer { get; set; }
    }
}

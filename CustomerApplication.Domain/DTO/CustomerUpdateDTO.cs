using System.ComponentModel.DataAnnotations;

namespace CustomerApplication.Domain.DTO
{
    public class CustomerUpdateDTO
    {
        [Required(ErrorMessage = "CustomerId is required")]
        public int CustomerIdnumber { get; set; }
        public string? FullName { get; set; }
        public string? Title { get; set; }
        [Phone]
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public int? SalesId { get; set; }
    }
}

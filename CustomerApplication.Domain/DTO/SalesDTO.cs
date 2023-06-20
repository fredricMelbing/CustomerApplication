﻿using System.ComponentModel.DataAnnotations;

namespace CustomerApplication.Domain.DTO
{
    public class SalesDTO
    {        
        [Key]
        [Required]
        public string SalesId { get; set; }
        [Required(ErrorMessage = "Name are required")]
        public string FullName { get; set; }
        [Phone]
        [Required(ErrorMessage = "Mobile no. is required")]
        public string Phonenumber { get; set; }
        [Required(ErrorMessage = "Email. is required")]
        [EmailAddress]
        public string Email { get; set; }
    }
}

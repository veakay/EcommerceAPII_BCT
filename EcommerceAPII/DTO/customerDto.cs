using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceAPI.DTO
{
    public class customerDto
    {
        public int CustomerId { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string BillingAddress { get; set; }
        public string DefaultAddress { get; set; }
        public string Country { get; set; }
    }
}

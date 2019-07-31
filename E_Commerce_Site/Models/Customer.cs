using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce_Site.Models
{
    public class Customer 
    {
        
     
        [Key]
        public int CustomerId { get; set; }
        [Required]
        public String CustomerName { get; set; }
        [Required]
        public String Address { get; set; }
        [Required]
        public String PostalCode { get; set; }
        [Required]
        public String Country { get; set; }
        [Required]
        public String City { get; set; }
        [Required]
        public String State { get; set; }
        [Required]
        public String PhoneNumber { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        public string CustomerNotes { get; set; }

        [Required]
        [EmailAddress]
        public String Email { get; set; }


        public DateTimeOffset Created { get; set; }
        public DateTimeOffset Updated { get; set; }


        public List <Order> OrderList { get; set; }
    }
}
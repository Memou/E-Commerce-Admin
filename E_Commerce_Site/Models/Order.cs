using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace E_Commerce_Site.Models
{
    public class Order 
    {
        [Key]
        public int OrderId { get; set; }
        [Required]
        public Decimal TotalCost { get; set; }
        [Required]
        public Decimal NetCost { get; set; }
        [Required]
        public Decimal TaxCost { get; set; }
        [Required]
        public Decimal ShippingCost { get; set; }
        public string Status { get; set; }
        public string OrderNotes { get; set; }


        public DateTimeOffset Created { get; set; }
        public DateTimeOffset Updated { get; set; }


        public virtual List<Inventory> InventoryList { get; set; }

        [Required]
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        


    }
}
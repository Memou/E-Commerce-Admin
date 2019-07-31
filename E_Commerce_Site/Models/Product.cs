using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace E_Commerce_Site.Models
{

    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        [StringLength(400)]
        public String ProductName { get; set; }
        [DisplayName("Canadian Price")]
        public decimal PriceCad { get; set; }
        [DisplayName("Rupee Price")]
        public decimal PriceInr { get; set; }
        public string ProductDescription { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Range(0, 100)]
        public int Discount { get; set; }
        public decimal ShippingCost { get; set; }
        public Boolean IsVisibleOnHome { get; set; }

        public double TotalRating { get; set; }



        //auto generated
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset Updated { get; set; }


        //fk
        public int SubCategoryId { get; set; }
        public virtual SubCategory SubCategory { get; set; }


        //navigational properties
        public virtual List<Color> ColorList { get; set; }
        public virtual List<Size> SizeList { get; set; }
        public virtual List<Review> ReviewList { get; set; }
        public virtual List<Tag> TagList { get; set; }
        public virtual List<Order> OrderList { get; set; }
        public virtual List<Inventory> InventoryList { get; set; }
        public virtual List<Image> ImageList { get; set; }




    }
}

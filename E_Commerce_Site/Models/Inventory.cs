using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_Commerce_Site.Models
{
    public class Inventory
    {
        [Key]
        public int InventoryId { get; set; }

        //fk
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        //fk
        public int SizeId { get; set; }
        public virtual Size Size { get; set; }

        //fk
        public int ColorId { get; set; }
        public virtual Color Color { get; set; }

        public int Quantity { get; set; }

    }
}
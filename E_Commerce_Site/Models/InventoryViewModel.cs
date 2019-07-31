using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Commerce_Site.Models
{
    public class InventoryViewModel
    {
        public int? ProductId { get; set; }

        public List<Color> ColorList { get; set; }
        
        public List<Size> SizeList { get; set; }

        public int? Quantity { get; set; }

        public Order Order { get; set; }

        public List<Inventory> InventoryList { get; set; }



    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace E_Commerce_Site.Models
{
    public class Tag
    {
        [Key]
        public int TagId { get; set; }
        public String TagName { get; set; }


        public virtual List<Product> ProductList { get; set; }

    }
}
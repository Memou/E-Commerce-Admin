using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Commerce_Site.Models
{
    public class SubCategory
    {
        public int SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
       // public Boolean IsVisibleOnHome { get; set; }

        //fk
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public virtual List<Product> ProductList { get; set; }

        //public SubCategory()
        //{

        //}

        //public SubCategory(String asd,int dsa)
        //{
        //    SubCategoryName = asd;
        //    CategoryId = dsa;
        //}
    }
}
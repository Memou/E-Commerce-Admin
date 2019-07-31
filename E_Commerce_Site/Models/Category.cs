using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections;

namespace E_Commerce_Site.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public String CategoryName { get; set; }

      //  public Boolean IsVisibleOnHome { get; set; }



        public virtual List<SubCategory> SubCategories { get; set; }

        //public Category()
        //{

        //}
        //public Category(String asd)
        //{
        //    CategoryName = asd;
        //}
    }
}
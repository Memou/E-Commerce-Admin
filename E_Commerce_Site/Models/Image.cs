using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace E_Commerce_Site.Models
{
    public class Image
    {   [Key]
        public int ImageId { get; set; }
        public String ImageAngle { get; set; }

        //[ForeignKey("Color")]
        public int ColorId { get; set; }
        public Color Color { get; set; }

        public String Address { get; set; }

        //[ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }



    }


} 
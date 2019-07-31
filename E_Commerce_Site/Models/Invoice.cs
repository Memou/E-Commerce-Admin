using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce_Site.Models
{
    public class Invoice
    {
        [Key]
        public int InvoiceId { get; set; }
        public bool Printed { get; set; }
        public bool Canceled { get; set; }
        public String Status { get; set; }

        [Required]
        public int OrderId { get; set; }
        public virtual Order Orders { get; set; }

        public DateTimeOffset Created { get; set; }
        public DateTimeOffset Updated { get; set; }
    }

    }
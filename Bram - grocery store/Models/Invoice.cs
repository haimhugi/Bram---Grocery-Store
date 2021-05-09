using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bram___grocery_store.Models
{
    public class Invoice
    {
        [Required]
        public int UserID { get; set; }
        [Required]
        public int InvoiceID { get; set; }

        [Required]
        public DateTime PurchaseDate { get; set; }

        [Required]
        public double Amount { get; set; }

        [Required]
        public int CategoryID { get; set; }

        public User user { get; set; }
    }
}

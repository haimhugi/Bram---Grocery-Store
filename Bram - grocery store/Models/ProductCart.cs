using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bram___grocery_store.Models
{
    public class ProductCart
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int CartId { get; set; }
        public Cart Cart { get; set; }

        [Display(Name = "Amount in Cart: ")]
        public int Amount { get; set; }

        [Display(Name = "The Final Price is: ")]
        public float FinalPrice { get; set; }
    }
}

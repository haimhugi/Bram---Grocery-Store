using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Bram___grocery_store.Models
{
    public class Product 
    {
        public int Id { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        public int Price { get; set; }

        [Display(Name = "Photo Product Url: ")]
        public string PhotoUrl { get; set; }

        public List<ProductCart> ProductCarts { get; set; }

    }
}

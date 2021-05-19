using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Bram___grocery_store.Models
{
    public class Sale
    {

        public int Id { get; set; }

        [Display(Name = "Sale Name is: ")]
        [StringLength(50)]
        [Required(ErrorMessage = "Name is required")]
        public string SaleName { get; set; }

        [Display(Name = "The Discount Percentage")]
        public int DiscountPercentage { get; set; }

        public List<Category> CategoriesOnSale { get; set; }
    }
}

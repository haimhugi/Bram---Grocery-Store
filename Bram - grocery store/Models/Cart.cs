using Microsoft.EntityFrameworkCore;
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
    public class Cart
    {


        [Display(Name = "Choose User: ")]
        [Key]
        public int UserId { get; set; }
        
        public User User { get; set; }

        

        [Display(Name = "The Cart Paid?")]
        public bool IsPaid { get; set; }

        [DefaultValue(0)]
        [Display(Name = "Total Cart Price")]
        public float TotalCartPrice { get; set; }

        public ObservableCollection<ProductCart> ProductsCart { get; set; }

    }
}

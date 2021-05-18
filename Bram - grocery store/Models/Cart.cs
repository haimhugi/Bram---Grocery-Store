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
        [Required]
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public ObservableCollection<ProductCart> Products { get; set; }

        public bool IsPaid { get; set; }

        public float TotalCartPrice { get; set; }

    }
}

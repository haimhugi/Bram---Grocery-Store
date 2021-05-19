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
    public class Category
    {
        public int Id { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Name is required")]

        public string Name { get; set; }

        public List<Product> Products { get; set; }
    }
}
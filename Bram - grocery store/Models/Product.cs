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

        [MaxLength(20)]
        [Required]
        public string Name { get; set; }

        [MaxLength(20)]
        public int Price { get; set; }

        public string PhotoUrl { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
        
    }
}

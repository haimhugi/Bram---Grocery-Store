using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bram___grocery_store.Models
{
    public class Product
    {

        public int Id { get; set; }

        [MaxLength(20)]
        [Required]
        public string Name { get; set; }

        [Range(0, 10000)]
        [Required]
        public int Price { get; set; }
        
        public string PhotoLink { get; set; }
        
        public Category Category { get; set; }
    }
}

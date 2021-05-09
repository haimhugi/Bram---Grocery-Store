using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bram___grocery_store.Models
{
    public class Category
    {
        public int Id { get; set; }

        [MaxLength(20)]
        [Required]
        public string Name { get; set; }

        public List<Product> Products { get; set; }

        public Sales MySale { get; set; }

    }
}

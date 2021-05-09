using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bram___grocery_store.Models
{
    public class Sales
    {
        public int IdSale { get; set; }

        public string SaleName { get; set; }

        public int DiscountPercentage { get; set; }

        public List<Category> CategoriesOnSale { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Bram___grocery_store.Models
{
    public class Sales
    {

        public int Id { get; set; }

        public string SaleName { get; set; }

        public int DiscountPercentage { get; set; }

        public List<Category> CategoriesOnSale { get; set; }
    }
}

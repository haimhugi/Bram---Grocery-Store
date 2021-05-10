using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bram___grocery_store.Models
{
    public class ProductCart : Product
    {
        public int Amount { get; set; }

        public float FinalPrice { get;
            //if(Amount > 0) {FinalPrice = Amount * this.Price* DiscountPercentage}
            //Sales!
            set;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bram___grocery_store.Models
{
    public class ProductCart : Product
    {
        private float _finalPrice;
        private int _amount;

        public int Amount {
            get => _amount; 
            set {
                _amount = value;
                if (Amount > 0)
                {
                    _finalPrice = Amount * Price; //* DiscountPercentage
                }
            }
        }

        public float FinalPrice {
            get => _finalPrice;
            private set => _finalPrice = value;
        }
        //Sales!
    }
}

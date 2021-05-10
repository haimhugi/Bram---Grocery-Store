using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Bram___grocery_store.Models
{
    public class Cart: System.ComponentModel.INotifyPropertyChanged
    {
        public int Id { get; set; }

        public User user { get; set; }

        public ICollection<ProductCart> products { get; set; }

        public bool Paid { get; set; }

        public float TotalCartPrice { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

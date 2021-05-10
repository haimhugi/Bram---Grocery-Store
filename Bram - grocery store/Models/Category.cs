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
    public class Category : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly int IdCounter = 0;
        private int _id;
        private string _name;
        private ObservableCollection<Product> _products;
        private Sales _currentSale;

        public int Id {
            get => _id; 
            private set {
                _id = value;
                OnPropertyChanged();
            }
        }

        [MaxLength(20)]
        [Required]
        public string Name {
            get => _name; 
            set {
                _name = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Product> Products {
            get => _products; 
            set {
                _products = value;
                OnPropertyChanged();
            }
        }

        public Sales CurrentSale {
            get => _currentSale; 
            set {
                _currentSale = value;
                OnPropertyChanged();
            }
        }

        public Category()
        {
            Id = ++IdCounter;
            Name = string.Empty;
            Products = new ObservableCollection<Product>();
            CurrentSale = new Sales();
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}

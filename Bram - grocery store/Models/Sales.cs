using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Bram___grocery_store.Models
{
    public class Sales : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly int IdCounter = 0;
        private int _id;
        private string _saleName;
        private float _discountPercentage;
        private ObservableCollection<Category> _categoriesOnSale;

        public int Id {
            get => _id;
            private set {
                _id = value;
                OnPropertyChanged();
            }
        }

        public string SaleName {
            get => _saleName; 
            set {
                _saleName = value;
                OnPropertyChanged();
            }
        }

        public float DiscountPercentage {
            get => _discountPercentage; 
            set {
                _discountPercentage = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Category> CategoriesOnSale {
            get => _categoriesOnSale; 
            set {
                _categoriesOnSale = value;
                OnPropertyChanged();
            }
        }

        public Sales()
        {
            Id = ++IdCounter;
            SaleName = string.Empty;
            //1 is the original price, anything more/less will be a decimal point
            DiscountPercentage = 1;
            CategoriesOnSale = new ObservableCollection<Category>();
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Bram___grocery_store.Models
{
    public class Product : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly int IdCounter = 0;
        private int _id;
        private string _name;
        private int _price;
        private string _photoUrl;
        private Category category;

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

        [Range(0, 10000)]
        [Required]
        public int Price {
            get => _price; 
            set {
                _price = value;
                OnPropertyChanged();
            }
        }
        
        public string PhotoUrl {
            get => _photoUrl; 
            set {
                _photoUrl = value;
                OnPropertyChanged();
            }
        }
        
        public Category Category {
            get => category; 
            set {
                category = value;
                OnPropertyChanged();
            }
        }

        public Product()
        {
            Id = ++IdCounter;
            Name = string.Empty;
            Price = 0;
            PhotoUrl = string.Empty;
            Category = new Category();
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}

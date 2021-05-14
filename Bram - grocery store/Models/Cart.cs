using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Bram___grocery_store.Models
{
    public class Cart : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly int IdCounter = 0;
        private int _id;
        private User _user;
        private ObservableCollection<ProductCart> _products;
        private bool _isPaid;
        private float _totalCartPrice;

        public int Id {
            get => _id; 
            private set {
                _id = value;
                OnPropertyChanged();
            }
        }
        public int UserId { get; set; }
        public User User {
            get => _user; 
            set {
                _user = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ProductCart> Products {
            get => _products; 
            set {
                _products = value;
                OnPropertyChanged();
            }
        }
        
        public bool IsPaid {
            get => _isPaid; 
            set {
                _isPaid = value;
                OnPropertyChanged();
            }
        }

        public float TotalCartPrice {
            get => _totalCartPrice;
            set {
                _totalCartPrice = value;
                OnPropertyChanged();
            }
        }

        public Cart()
        {
            Id = ++IdCounter;
            User = new User();
            Products = new ObservableCollection<ProductCart>();
            IsPaid = false;
            TotalCartPrice = 0;
        }


 /*       protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cart>()
                .HasOne(a => a.User)
                .WithOne(b => b.Cart)
                .HasForeignKey<User>(b => b.Cart.Id);
        }*/

        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}

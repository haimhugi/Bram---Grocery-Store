using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bram___grocery_store.Models
{
    public class User
    {
        public int Id { get; set; }

        [StringLength(50)]
        [Required]
        [RegularExpression(@"^[A-Za-z\s]*$")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [RegularExpression(@"^[A-Za-z\s]*$")]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "User Name")]
        [StringLength(50)]
        [RegularExpression(@"^[A-Za-z0-9\s]*$")]
        public string UserName { get; set; }

        [Display(Name = "Password")]
        [Required]
        [RegularExpression(@"^[A-Za-z0-9\s]*$")]
        public string Password { get; set; }

        public bool IsAdmin { get; set; }

        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")]
        [Display(Name = "mail@mail.co.il")]
        public string Email { get; set; }

        public Cart Cart { get; set; }

    }
}


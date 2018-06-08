using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CafeCarki.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Lütfen E-Posta adresinizi yazın.")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Lütfen şifrenizi girin.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Beni Hatırla?")]
        public bool RememberMe { get; set; }
    }
}

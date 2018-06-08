using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CafeCarki.ViewModels
{
    public class RegisterViewModel
    {
        
        [Display(Name = "Ad")]
        public string Name { get; set; }

        [Display(Name = "Soyad")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Lütfen E-posta adresinizi girin.")]
        [EmailAddress(ErrorMessage = "Lütfen geçerli bir E-posta adresi girin.")]
        [Display(Name = "E-posta")]
        public string Email { get; set; }

        [Display(Name = "Cep Telefonu")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Lütfen şifrenizi girin.")]
        [StringLength(100, ErrorMessage = "{0} en az {2} karakter, en fazla {1} karakter uzunluğunda olmalı.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Şifre Tekrar")]
        [Compare("Password", ErrorMessage = "Şifreniz tekrarı ile uyuşmuyor.")]
        public string ConfirmPassword { get; set; }

       
    }
}

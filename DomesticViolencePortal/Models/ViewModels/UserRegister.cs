using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DomesticViolencePortal.Models.ViewModels
{
    public class UserRegister
    {
        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Логин")]
        [StringLength(15, MinimumLength = 5, ErrorMessage = "Логин должен содержать от 5 до 15 символов")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Пароль")]
        [StringLength(25, ErrorMessage = "Пароль должен содержать от 8 до 25 символов", MinimumLength = 8)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Подтвердите Пароль")]
        [StringLength(25, ErrorMessage = "Пароль должен содержать от 8 до 25 символов", MinimumLength = 8)]
        public string VerifyPassword { get; set; }

        [Display(Name = "Роль")]

        public string Role { get; set; }
        

    }
}
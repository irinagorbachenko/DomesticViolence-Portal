using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DomesticViolencePortal.Models
{
    public class Login
    {
        [Key]
        //[ForeignKey("User")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Логин")]
        public string UserLogin { get; set; }

        [Required]
        [Display(Name = "Пароль")]
        public byte[] Password { get; set; }
    }
}
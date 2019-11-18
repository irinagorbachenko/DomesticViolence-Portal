using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;

namespace DomesticViolencePortal.Models
{
    public class Donation
    {

        public int DonationId { get; set; }
        
        [DisplayName("Имя")]
        public string FirstName { get; set; }
        [DisplayName("Имя")]
        public string LastName { get; set; }
        [DisplayName("Почта")]
        public string Email { get; set; }
        [DisplayName("Могу помочь")]
        public string Theme { get; set; }
        [DisplayName("Заголовок")]
        public string Title { get; set; }
        [DisplayName("Текст")]
        public string Text { get; set; }
        [DisplayName("Фото")]
        public string Image { get; set; }
        [DisplayName("Дата")]
        public DateTime Date { get; set; }
       
        public int ?UserId { get; set; }
        public virtual User User { get; set; }
        
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }

    }
}
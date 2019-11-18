using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace DomesticViolencePortal.Models
{
    public class NeedHelpUser
    {

        public int NeedHelpUserId { get; set; }

        [DisplayName("Имя")]
        public string FirstName { get; set; }
        [DisplayName("Имя")]
        public string LastName { get; set; }
        [DisplayName("Почта")]
        public string Email { get; set; }
        [DisplayName("Мне нужна помощь")]
        public string Theme { get; set; }
        [DisplayName("Заголовок")]
        public string Title { get; set; }
        [DisplayName("Текст")]
        public string Text { get; set; }
        [DisplayName("Фото")]
        public string Image { get; set; }
        [DisplayName("Дата")]
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
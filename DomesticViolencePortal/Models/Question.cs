using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace DomesticViolencePortal.Models
{
    public class Question
    {
        public int QuestionId { get; set; }
        public int? UserId { get; set; }
        [DisplayName("Имя")]
        public string FirstName { get; set; }
        [DisplayName("Почта")]
        public string Email { get; set; }

        [DisplayName("Заголовок")]
        public string Title { get; set; }
        [DisplayName("Сообщение")]
        public string Message { get; set; }


        public virtual ICollection<User> Users { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace DomesticViolencePortal.Models
{
    public class Post
    {
        public int PostId { get; set; }
        [DisplayName("Заголовок")]
        public string Title { get; set; }
        [DisplayName("Текст")]
        public string Text { get; set; }
        [DisplayName("Фото")]
        public string Image { get; set; }
        public int? UserId { get; set; }
        [DisplayName("Дата")]
        public DateTime Date { get; set; }

        public virtual User User { get; set; }
       

        public virtual ICollection<Comment> Comments { get; set; }
    }
}

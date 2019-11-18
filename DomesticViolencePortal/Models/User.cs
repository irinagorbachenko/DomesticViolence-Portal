using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomesticViolencePortal.Models
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        [DisplayName("Фамилия")]
        public string LastName { get; set; }
        [DisplayName(" Имя")]
        public string FirstName { get; set; }
        [DisplayName("Почта")]
        public string Email { get; set; }
        [DisplayName("Логин")]
        [ForeignKey("Login")]
        public int LoginId { get; set; }
        public virtual Login Login { get; set; }
        [DisplayName("Фото")]
        public string Image { get; set; }
        
        [ForeignKey("Role")]
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<NeedHelpUser> NeedHelpUsers { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

      
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace DomesticViolencePortal.Models
{
    public class Role
    {
        public int Id { get; set; }
        [DisplayName("Роль")]
        public string RoleName { get; set; }

        public virtual ICollection<User> Users { get; set; }
        public Role()
        {
            Users = new List<User>();
        }
    }
}
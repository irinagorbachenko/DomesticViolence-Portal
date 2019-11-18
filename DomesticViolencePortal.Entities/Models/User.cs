using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.Models
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }

        [ForeignKey("Login")]
        public int LoginId { get; set; }
        public virtual Login Login { get; set; }

        [ForeignKey("Role")]
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
    }
}
using DomesticViolencePortal.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Entity;


namespace DomesticViolencePortal.DAL
{
  public  class DomesticViolenceContext : DbContext
    {
        public DomesticViolenceContext() : base("DomesticViolence")
        {

        }


        public DbSet<Role> Roles { get; set; }
        public DbSet<Login> Logins { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Volonteer> Volonteers { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Donation> Donations { get; set; }
        public DbSet<NeedHelpUser> NeedHelpUsers { get; set; }

        





    }


    

}

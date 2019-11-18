using Hospital.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Entity;

namespace DomesticViolencePortal.DAL
{
    class DomesticViolenceContext : DbContext
    {
        public DomesticViolenceContext() : base("DomesticViolence")
        {

        }

       
        public DbSet<User> Users { get; set; }
       

    }
    
}

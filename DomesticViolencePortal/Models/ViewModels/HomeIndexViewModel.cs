using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DomesticViolencePortal.Models.ViewModels
{
    public class HomeIndexViewModel
    {
        public List<String> Pictures { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public List<Post> Posts { get; set; }
        public List<Donation> Donations { get; set; }
        public Volonteer Volonteer { get; set; }
        public Question Question { get; set; }

        public HomeIndexViewModel()
        {
            Pictures = new List<string>();
            Posts = new List<Post>();
            Donations=new List<Donation>();

           this.Volonteer=new Volonteer();
            this.Question = new Question();
        }
    }
    
}
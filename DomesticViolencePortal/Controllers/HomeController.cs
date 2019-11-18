using DomesticViolencePortal.DAL;
using DomesticViolencePortal.Models;
using DomesticViolencePortal.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DomesticViolencePortal.Controllers
{
    public class HomeController : Controller
    {
        private DomesticViolenceContext db = new DomesticViolenceContext();
        HomeIndexViewModel homeModel = new HomeIndexViewModel();

        public ActionResult Index(HomeIndexViewModel homeModel)
        {
            string galeryPath = $"{Request.PhysicalApplicationPath}Content/images/Gallery/";
            var dir = new DirectoryInfo(galeryPath);
            FileInfo[] files = dir.GetFiles();
            files = files.OrderByDescending(x => x.CreationTime).Take(8).ToArray();

            foreach (var file in files)
            {
                string pathToImage = $"/Content/images/Gallery/{file.Name}";
                homeModel.Pictures.Add(pathToImage);
                
            }
            
       
            homeModel.Posts = db.Posts.Include(post => post.User).Take(3).ToList();
            homeModel.Donations = db.Donations.Include(donation => donation.User).Take(3).ToList();
            
            return View(homeModel);
        }

        public ActionResult CreateVolonteer (Volonteer volonteer)
        {
            if (ModelState.IsValid)
            {
                if (this.Session["IsAuth"]!=null && (bool)this.Session["IsAuth"]==true)
                {
                    volonteer.FirstName = (string)this.Session["CurrentUserName"];
                    volonteer.Email = (string)this.Session["CurrentUserEmail"];
                    volonteer.UserId = (int?)this.Session["CurrentUser"];
                }
          

                db.Volonteers.Add(volonteer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
    
            return View();

        }


        public ActionResult CreateQuestion(Question question)
        {
            if (ModelState.IsValid)
            {
                if (this.Session["IsAuth"] != null && (bool)this.Session["IsAuth"] == true)
                {
                    question.FirstName = (string)this.Session["CurrentUserName"];
                    question.Email = (string)this.Session["CurrentUserEmail"];
                    question.UserId = (int?)this.Session["CurrentUser"];
                }


                db.Questions.Add(question);
                db.SaveChanges();
                return RedirectToAction("Contact");
            }

            return View("Contact");

        }

        public ActionResult About()
        {
            homeModel.Donations = db.Donations.Include(donation => donation.User).Take(3).ToList();

            return View(homeModel);
        }

        public ActionResult Contact()
        {
            

            return View();
        }


        public ActionResult Chat()
        {
            return View();
        }

        public ActionResult Donate()
        {
            return View();
        }


        public ActionResult RecentBlogs( )
        {
            var a = db.Posts.ToList();
           
               var post = a.OrderByDescending(x => x.Date).Take(2).ToArray();
                return PartialView("RecentBlogs",post);
           
            

        }

        public ActionResult RecentDonations()
        {
            var a = db.Donations.ToList();

            var donation = a.OrderByDescending(x => x.Date).Take(2).ToArray();
            return PartialView("RecentDonations",donation);



        }


    }
}
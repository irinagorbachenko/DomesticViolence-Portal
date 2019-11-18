using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DomesticViolencePortal.DAL;
using System.Security.Cryptography;
using DomesticViolencePortal.Models.ViewModels;
using DomesticViolencePortal.Providers;
using DomesticViolencePortal.Models;
using System.Data.Entity;
using System.IO;

namespace DomesticViolencePortal.Controllers
{
    public class AccountController : Controller
    {
        private DomesticViolenceContext db = new DomesticViolenceContext();
        public string WelcomeMsg(string input)
        {
            if (!String.IsNullOrEmpty(input))
                return "Please welcome " + input + ".";
            else
                return "Please enter your name.";
        }


        // GET: Account
        [AllowAnonymous] //доступ у всех есть
        public ActionResult Login()
        {
            return View();
        }

       




        [AllowAnonymous] //доступ у всех есть
        public ActionResult PersonalCabinet()
        {
            if (Session["IsAuth"]!=null&&(bool) Session["IsAuth"])
            {

                
               int userId = (int)this.Session["CurrentUser"];
               

                User currentUserFromDb = db.Users.First(u => u.UserId == userId);
                return View(currentUserFromDb);
            }
            else
            {
                

                return RedirectToAction("Login");
            }
        
        }

        public ActionResult UpoladPictures(HttpPostedFileBase uploadImage)
        {
            using (var binaryReader = new BinaryReader(uploadImage.InputStream))
            {
                Random r = new Random();
                string fileName = $"/Content/images/Gallery/{r.Next()}{uploadImage.FileName}";
                string savePath = $"{Request.PhysicalApplicationPath}{fileName}";
                System.IO.File.WriteAllBytes(savePath, binaryReader.ReadBytes(uploadImage.ContentLength));

                int CurrentUserId = (int)this.Session["CurrentUser"];
                User user = db.Users.First(u => u.UserId == CurrentUserId);
                user.Image = fileName;
                db.SaveChanges();
            }
            return RedirectToAction("PersonalCabinet");
        }

      

        public ActionResult EditUser( User user,string Password)
        {
    
            user.Login.Password = new System.Security.Cryptography.MD5CryptoServiceProvider().ComputeHash(
                System.Text.Encoding.Unicode.GetBytes(Password));
          
            db.Entry(user.Login).State = EntityState.Modified;
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();

            
            int UserIdFromDb = user.UserId;
            this.Session.Add("CurrentUser", user.UserId);         
            this.Session.Add("CurrentUserPassword", Password);
           


            return RedirectToAction("PersonalCabinet");
            
            ViewBag.UserId = new SelectList(db.Users, "UserId", "LastName", user.FirstName,user.LastName,user.Image);
            return View("PersonalCabinet");
        }

        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public ActionResult Login(string Login, string Password, string returnUrl)
        {
            //if (ModelState.IsValid)
            //{
            //}
            var user = new UserRegister() { Login = Login, Password = Password };
         
            if (Membership.ValidateUser(Login, Password))
            {

                User User = db.Users.First(x => x.Login.UserLogin == Login);
                string Image = User.Image;
                string Email = User.Email;            
                string FirstName = User.FirstName;
                string LastName = User.LastName;
                string Password1 = Password;
                int UserIdFromDb = User.UserId;

                this.Session.Add("Image", Image);
                this.Session.Add("CurrentUserEmail", Email);
                this.Session.Add("CurrentUserName", FirstName);
                this.Session.Add("CurrentUserLastName", LastName);
                this.Session.Add("CurrentUser", UserIdFromDb);
                this.Session.Add("CurrentUserLogin", Login);
                this.Session.Add("CurrentUserPassword", Password1);
                this.Session.Add("Authorization", user);             
                this.Session.Add("IsAuth", true);

                   //вытащить  текущего  пользователя с бд
                   //если он  админ  записать в сесию isAdmin  true;
                if (user.Login=="Admin")
                {
                    this.Session.Add("IsAdmin", true);
                }
                //var  userNew=
                //this.Session.Add("Привет", userNew);

            
                    return RedirectToAction("Index", "Home");
                
            }

            return View(user);
        }
        DomesticViolenceContext _context = new DomesticViolenceContext();

       // [Authorize(Roles = "Admin")] //только авторизированный пользователь
        public ActionResult Register()
        {
            ViewBag.Roles = new SelectList(_context.Roles.AsEnumerable().Select(r => r.RoleName));
            return View();
        }


        [HttpPost]
        public ActionResult Register(UserRegister user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    System.Security.Cryptography.MD5 mD5 = new MD5CryptoServiceProvider();
                    byte[] password = mD5.ComputeHash(System.Text.Encoding.Unicode.GetBytes(user.Password));
                    user.Role = "Guest";


                    MembershipUser memberShip = (Membership.Provider as CustomMembershipProvider).CreateUser(user.LastName, user.FirstName, user.Login, password,user.Role);

                    if (memberShip != null)
                    {
                        // return RedirectToAction("Login");
                        return Content("true");
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }
            ViewBag.Roles = _context.Roles.Where(r => r.RoleName == "Guest");
            //return View();

            return Content("false");


            // return RedirectToAction("Register");
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Login");
        }


        
        public ActionResult CreatePostfromUser()
        {
            
            return RedirectToAction("Create", "Posts");
        }

    }
}

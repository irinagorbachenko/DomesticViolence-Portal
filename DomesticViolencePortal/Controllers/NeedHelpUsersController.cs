using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DomesticViolencePortal.DAL;
using DomesticViolencePortal.Models;

namespace DomesticViolencePortal.Controllers
{
    public class NeedHelpUsersController : Controller
    {
        private DomesticViolenceContext db = new DomesticViolenceContext();


        public ActionResult Index()
        {
            var NeedHelpUsers = db.NeedHelpUsers.Include(p => p.User).ToList();

            return View(NeedHelpUsers);
        }


        // GET: Posts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NeedHelpUser needHelpUser = db.NeedHelpUsers.Find(id);
            if (needHelpUser == null)
            {
                return HttpNotFound();
            }
            return View(needHelpUser);
        }

        // GET: Posts/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "UserId", "LastName");
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.





        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NeedHelpUser needHelpUser)
        {
            if (ModelState.IsValid)
            {

               
                needHelpUser.Image= (string)this.Session["Image"];
                needHelpUser.UserId = (int)this.Session["CurrentUser"];
                needHelpUser.Date = DateTime.Now;
                db.NeedHelpUsers.Add(needHelpUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users, "UserId", "LastName", needHelpUser.UserId);
            return View(needHelpUser);
        }

        // GET: Posts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NeedHelpUser needHelpUser = db.NeedHelpUsers.Find(id);
            if (needHelpUser == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "LastName", needHelpUser.UserId);
            return View(needHelpUser);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( NeedHelpUser needHelpUser)
        {
            if (ModelState.IsValid)
            {
                //using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                //{
                //    Random r = new Random();
                //    string fileName = $"/Content/images/{r.Next()}{uploadImage.FileName}";
                //    string savePath = $"{Request.PhysicalApplicationPath}{fileName}";
                //    System.IO.File.WriteAllBytes(savePath, binaryReader.ReadBytes(uploadImage.ContentLength));
                //    needHelpUser.Image = fileName;
                //}
                needHelpUser.Image = (string)this.Session["Image"];
                needHelpUser.UserId = (int)this.Session["CurrentUser"];
                needHelpUser.Date = DateTime.Now;
                db.Entry(needHelpUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = this.Session["CurrentUser"] });
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "LastName", needHelpUser.UserId);
            return View(needHelpUser);
        }

        // GET: Posts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NeedHelpUser needHelpUser = db.NeedHelpUsers.Find(id);
            if (needHelpUser == null)
            {
                return HttpNotFound();
            }
            return View(needHelpUser);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmedForUser(int id)
        {
        
         NeedHelpUser needHelpUser = db.NeedHelpUsers.Find(id);
            db.NeedHelpUsers.Remove(needHelpUser);
            db.SaveChanges();

            return RedirectToAction("Index", new { id = this.Session["CurrentUser"] });
        }


        ///ADMINSECTION

        // GET: NeedHelpUsers1
        public ActionResult IndexForAdmin()
        {
            return View(db.NeedHelpUsers.ToList());
        }

        // GET: NeedHelpUsers1/Details/5
        public ActionResult DetailsIndexForAdmin(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NeedHelpUser needHelpUser = db.NeedHelpUsers.Find(id);
            if (needHelpUser == null)
            {
                return HttpNotFound();
            }
            return View(needHelpUser);
        }

        // GET: NeedHelpUsers1/Create
        public ActionResult CreateIndexForAdmin()
        {
            return View();
        }

        // POST: NeedHelpUsers1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateIndexForAdmin([Bind(Include = "NeedHelpUserId,FirstName,LastName,Email,Theme,Title,Text,Image,Date,UserId")] NeedHelpUser needHelpUser)
        {
            if (ModelState.IsValid)
            {
                db.NeedHelpUsers.Add(needHelpUser);
                db.SaveChanges();
                return RedirectToAction("IndexForAdmin");
            }

            return View(needHelpUser);
        }

        // GET: NeedHelpUsers1/Edit/5
        public ActionResult EditIndexForAdmin(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NeedHelpUser needHelpUser = db.NeedHelpUsers.Find(id);
            if (needHelpUser == null)
            {
                return HttpNotFound();
            }
            return View(needHelpUser);
        }

        // POST: NeedHelpUsers1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditIndexForAdmin([Bind(Include = "NeedHelpUserId,FirstName,LastName,Email,Theme,Title,Text,Image,Date,UserId")] NeedHelpUser needHelpUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(needHelpUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("IndexForAdmin");
            }
            return View(needHelpUser);
        }

        // GET: NeedHelpUsers1/Delete/5
        public ActionResult DeleteIndexForAdmin(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NeedHelpUser needHelpUser = db.NeedHelpUsers.Find(id);
            if (needHelpUser == null)
            {
                return HttpNotFound();
            }
            return View(needHelpUser);
        }

        // POST: NeedHelpUsers1/Delete/5
        [HttpPost, ActionName("DeleteIndexForAdmin")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NeedHelpUser needHelpUser = db.NeedHelpUsers.Find(id);
            db.NeedHelpUsers.Remove(needHelpUser);
            db.SaveChanges();
            return RedirectToAction("IndexForAdmin");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

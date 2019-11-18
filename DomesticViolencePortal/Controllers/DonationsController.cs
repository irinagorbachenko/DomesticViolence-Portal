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
    public class DonationsController : Controller
    {
        private DomesticViolenceContext db = new DomesticViolenceContext();

        


        public ActionResult Index()
    {
        var donation = db.Donations.Include(p => p.User).ToList();
        
        return View(donation);
    }


    // GET: Posts/Details/5
    public ActionResult Details(int? id)
    {
        if (id == null)
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        Donation donation = db.Donations.Find(id);
        if (donation == null)
        {
            return HttpNotFound();
        }
        return View(donation);
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
    public ActionResult Create(Donation donation)
    {
        if (ModelState.IsValid)
        {

            //using (var binaryReader = new BinaryReader(uploadImage.InputStream))
            //{
            //    Random r = new Random();
            //    string fileName = $"/Content/images/{r.Next()}{uploadImage.FileName}";
            //    string savePath = $"{Request.PhysicalApplicationPath}{fileName}";
            //    System.IO.File.WriteAllBytes(savePath, binaryReader.ReadBytes(uploadImage.ContentLength));
            //    donation.Image = fileName;
            //}
            donation.Image =(string) this.Session["Image"];
            donation.UserId = (int)this.Session["CurrentUser"];
            donation.Date = DateTime.Now;
            db.Donations.Add(donation);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        ViewBag.UserId = new SelectList(db.Users, "UserId", "LastName", donation.UserId);
        return View(donation);
    }

    // GET: Posts/Edit/5
    public ActionResult Edit(int? id)
    {
        if (id == null)
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        Donation donation = db.Donations.Find(id);
        if (donation == null)
        {
            return HttpNotFound();
        }
        ViewBag.UserId = new SelectList(db.Users, "UserId", "LastName", donation.UserId);
        return View(donation);
    }

    // POST: Posts/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit( Donation donation)
    {
        if (ModelState.IsValid)
        {
                //using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                //{
                //    Random r = new Random();
                //    string fileName = $"/Content/images/{r.Next()}{uploadImage.FileName}";
                //    string savePath = $"{Request.PhysicalApplicationPath}{fileName}";
                //    System.IO.File.WriteAllBytes(savePath, binaryReader.ReadBytes(uploadImage.ContentLength));
                //    donation.Image = fileName;
            //}
            donation.Image = (string)this.Session["Image"];
            donation.UserId = (int)this.Session["CurrentUser"];
            donation.Date = DateTime.Now;
            db.Entry(donation).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", new { id = this.Session["CurrentUser"] });
        }
        ViewBag.UserId = new SelectList(db.Users, "UserId", "LastName", donation.UserId);
        return View(donation);
    }

    // GET: Posts/Delete/5
    public ActionResult Delete(int? id)
    {
        if (id == null)
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        Donation donation = db.Donations.Find(id);
            if (donation == null)
        {
            return HttpNotFound();
        }
        return View(donation);
    }

    // POST: Posts/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmedForUser(int id)
    {
        Donation donation = db.Donations.Find(id);
        db.Donations.Remove(donation);
        db.SaveChanges();

        return RedirectToAction("Index", new { id = this.Session["CurrentUser"] });
    }

        //Admin section//////////////////////////////////////////////////////////////////////////////////////////////



        public ActionResult IndexForAdmin()
        {
            var donations = db.Donations.Include(p => p.User);
            return View(donations.ToList());


        }


        // GET: Posts/Details/5
        public ActionResult DetailsForAdmin(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donation donations = db.Donations.Find(id);
            if (donations == null)
            {
                return HttpNotFound();
            }
            return View(donations);
        }

        // GET: Posts/Create
        public ActionResult CreateForAdmin()
        {
            ViewBag.UserId = new SelectList(db.Users, "UserId", "LastName");
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateForAdmin([Bind(Include = "PostId,Title,Text,Image,UserId,Date")] Donation donations)
        {
            if (ModelState.IsValid)
            {
                db.Donations.Add(donations);
                db.SaveChanges();
                return RedirectToAction("IndexForAdmin");
            }

            ViewBag.UserId = new SelectList(db.Users, "UserId", "LastName", donations.UserId);
            return View(donations);
        }

        // GET: Posts/Edit/5
        public ActionResult EditForAdmin(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donation donations = db.Donations.Find(id);
            if (donations == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "LastName", donations.UserId);
            return View(donations);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditForAdmin([Bind(Include = "PostId,Title,Text,Image,UserId,Date")] Donation donations)
        {
            if (ModelState.IsValid)
            {
                db.Entry(donations).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("IndexForAdmin");
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "LastName", donations.UserId);
            return View(donations);
        }

        // GET: Posts/Delete/5
        public ActionResult DeleteForAdmin(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donation donations = db.Donations.Find(id);
            if (donations == null)
            {
                return HttpNotFound();
            }
            return View(donations);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("DeleteForAdmin")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmedForAdmins(int id)
        {
            Donation donations = db.Donations.Find(id);
            db.Donations.Remove(donations);
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

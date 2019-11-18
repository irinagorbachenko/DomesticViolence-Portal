using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DomesticViolencePortal.DAL;
using DomesticViolencePortal.Models;

namespace DomesticViolencePortal.Controllers
{
    public class VolonteersController : Controller
    {
        private DomesticViolenceContext db = new DomesticViolenceContext();

        // GET: Volonteers
        public ActionResult Index()
        {
            return View(db.Volonteers.ToList());
        }

        // GET: Volonteers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Volonteer volonteer = db.Volonteers.Find(id);
            if (volonteer == null)
            {
                return HttpNotFound();
            }
            return View(volonteer);
        }

        // GET: Volonteers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Volonteers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VolonteerId,UserId,FirstName,Email,Message")] Volonteer volonteer)
        {
            if (ModelState.IsValid)
            {
                db.Volonteers.Add(volonteer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(volonteer);
        }

        // GET: Volonteers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Volonteer volonteer = db.Volonteers.Find(id);
            if (volonteer == null)
            {
                return HttpNotFound();
            }
            return View(volonteer);
        }

        // POST: Volonteers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VolonteerId,UserId,FirstName,Email,Message")] Volonteer volonteer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(volonteer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(volonteer);
        }

        // GET: Volonteers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Volonteer volonteer = db.Volonteers.Find(id);
            if (volonteer == null)
            {
                return HttpNotFound();
            }
            return View(volonteer);
        }

        // POST: Volonteers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Volonteer volonteer = db.Volonteers.Find(id);
            db.Volonteers.Remove(volonteer);
            db.SaveChanges();
            return RedirectToAction("Index");
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

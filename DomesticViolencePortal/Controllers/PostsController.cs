using System;
using System.Collections.Generic;
using DomesticViolencePortal.Models.ViewModels;
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
    public class PostsController : Controller
    {
        private DomesticViolenceContext db = new DomesticViolenceContext();

       [HttpGet]
        public ActionResult LeaveComment(int postId)
        {
            Comment comment = new Comment(){PostId = postId};
            ViewBag.PostId = postId;
            return PartialView("LeaveComment", comment);

                
        }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LeaveComment(Comment comment)
        {
            if (ModelState.IsValid)
            {
                //var a = db.Posts.ToList();

                //var post = a.OrderByDescending(x => x.Date).Take(2).ToArray();
                //return PartialView("RecentBlogs", post);
                db.Comments.Add(comment);
                db.SaveChanges();
                Comment AddedComment = db.Comments.Add(comment);
                AddedComment.User.FirstName = (((string) this.Session["CurrentUserName"]));
                AddedComment.User.LastName = (((string)this.Session["CurrentUserLastName"]));

                return PartialView("LeaveComment", comment);

                //return View("Details", AddedComment);
            }

            ViewBag.PostId = comment.PostId;

            ViewBag.UserId = new SelectList(db.Users, "UserId", "LastName", comment.UserId);
            return View(comment);
        }

        public ActionResult Partial(List<Comment>comments,Post p)
        {
            var a = db.Comments.ToList();
            
       
            comments = a.Where(comment=> comment.PostId==p.PostId).ToList();

            return PartialView(comments);

        }


        // GET: Posts
        public ActionResult Index()
        {

            var posts = db.Posts.Include(p => p.Comments).Include(p => p.User).ToList();
            return View(posts.ToList());
        }

      

        // GET: Posts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Post post = db.Posts.Include(x=>x.Comments).ToList().First(x=>x.PostId==id);

          
         
            if (post == null)
            {
                return HttpNotFound();
            }
            ViewBag.PostId = new SelectList(db.Posts, "PostId", "PostId", post.PostId == id);
            return View(post);
        }


        public ActionResult DetailsPreview(string filterName, string filterValue, int? id)
        {
            //if (filterValue == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}

            //= id;
            Post post = db.Posts.Find(id);


            //if (post == null)
            //{
            //    return HttpNotFound();
            //}
            return View(post);
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
        public ActionResult Create(Post post, HttpPostedFileBase uploadImage)
        {
            if (ModelState.IsValid)
            {

                using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                {
                    Random r = new Random();
                    string fileName = $"/Content/images/{r.Next()}{uploadImage.FileName}";
                    string savePath = $"{Request.PhysicalApplicationPath}{fileName}";
                    System.IO.File.WriteAllBytes(savePath, binaryReader.ReadBytes(uploadImage.ContentLength));
                    post.Image = fileName;
                }

                post.UserId = (int)this.Session["CurrentUser"];
                post.Date=DateTime.Now;
                db.Posts.Add(post);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users, "UserId", "LastName", post.UserId);
            return View(post);
        }






        // GET: Posts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "LastName", post.UserId);
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PostId,Title,Text,Image,UserId,Date")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "LastName", post.UserId);
            return View(post);
        }

        // GET: Posts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
            db.SaveChanges();
            return RedirectToAction("Index");
            
        }
        //Admin section////



        public ActionResult IndexForAdmins()
        {
            var posts = db.Posts.Include(p => p.User);
            return View(posts.ToList());


        }


        // GET: Posts/Details/5
        public ActionResult DetailsForAdmins(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: Posts/Create
        public ActionResult CreateForAdmins()
        {
            ViewBag.UserId = new SelectList(db.Users, "UserId", "LastName");
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateForAdmins([Bind(Include = "PostId,Title,Text,Image,UserId,Date")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Posts.Add(post);
                db.SaveChanges();
                return RedirectToAction("IndexForAdmins");
            }

            ViewBag.UserId = new SelectList(db.Users, "UserId", "LastName", post.UserId);
            return View(post);
        }

        // GET: Posts/Edit/5
        public ActionResult EditForAdmins(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "LastName", post.UserId);
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditForAdmins([Bind(Include = "PostId,Title,Text,Image,UserId,Date")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("IndexForAdmins");
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "LastName", post.UserId);
            return View(post);
        }

        // GET: Posts/Delete/5
        public ActionResult DeleteForAdmins(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("DeleteForAdmins")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmedForAdmins(int id)
        {
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
            db.SaveChanges();
            return RedirectToAction("IndexForAdmins");
        }


        ///USER SECTION
        ///


        public ActionResult IndexUser(int? id)
        {
            var a = db.Posts.Include(p => p.User).ToList();

            List<Post> posts = a.Where(x =>x.UserId == id).ToList(); 
            return View(posts);
        }


        // GET: Posts/Details/5
        public ActionResult DetailsForUser(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: Posts/Create
        public ActionResult CreateForUser()
        {
            ViewBag.UserId = new SelectList(db.Users, "UserId", "LastName");
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

      



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateForUser(Post post, HttpPostedFileBase uploadImage)
        {
            if (ModelState.IsValid)
            {

                using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                {
                    Random r = new Random();
                    string fileName = $"/Content/images/{r.Next()}{uploadImage.FileName}";
                    string savePath = $"{Request.PhysicalApplicationPath}{fileName}";
                    System.IO.File.WriteAllBytes(savePath, binaryReader.ReadBytes(uploadImage.ContentLength));
                    post.Image = fileName;
                }

                post.UserId = (int)this.Session["CurrentUser"];
                post.Date = DateTime.Now;
                db.Posts.Add(post);
                db.SaveChanges();
                return RedirectToAction("IndexUser");
            }

            ViewBag.UserId = new SelectList(db.Users, "UserId", "LastName", post.UserId);
            return View(post);
        }

        // GET: Posts/Edit/5
        public ActionResult EditForUser(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "LastName", post.UserId);
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditForUser(HttpPostedFileBase uploadImage, Post post)
        {
            if (ModelState.IsValid)
            {
                using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                {
                    Random r = new Random();
                    string fileName = $"/Content/images/{r.Next()}{uploadImage.FileName}";
                    string savePath = $"{Request.PhysicalApplicationPath}{fileName}";
                    System.IO.File.WriteAllBytes(savePath, binaryReader.ReadBytes(uploadImage.ContentLength));
                    post.Image = fileName;
                }

                post.UserId = (int)this.Session["CurrentUser"];
                post.Date = DateTime.Now;
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("IndexUser", new { id = this.Session["CurrentUser"] });
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "LastName", post.UserId);
            return View(post);
        }

        // GET: Posts/Delete/5
        public ActionResult DeleteForUser(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("DeleteForUser")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmedForUser(int id)
        {
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
            db.SaveChanges();

            return RedirectToAction("IndexUser" ,new { id=this.Session["CurrentUser"]});
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

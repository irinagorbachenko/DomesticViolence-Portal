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
using System.IO;

namespace DomesticViolencePortal.Controllers
{
    public class GalleryController : Controller
    {
        private DomesticViolenceContext db = new DomesticViolenceContext();

        public ActionResult Index()
        {
            List<String> galeryFilesPath = new List<string>();
            //1. Путь к галерее.
            string galeryPath = $"{Request.PhysicalApplicationPath}Content/images/Gallery/";
            var dir = new DirectoryInfo(galeryPath);
            FileInfo[] files = dir.GetFiles();
            files = files.OrderByDescending(x => x.CreationTime).ToArray();

            foreach (var file in files)
            {
                string pathToImage = $"/Content/images/Gallery/{file.Name}";
                galeryFilesPath.Add(pathToImage);
            }
            // Get the files in the directory and print out some information about them.




            return View(galeryFilesPath);
        }

        public ActionResult UpoladPictures(HttpPostedFileBase uploadImage)
        {
            using (var binaryReader = new BinaryReader(uploadImage.InputStream))
            {
                Random r = new Random();
                string fileName = $"/Content/images/Gallery/{r.Next()}{uploadImage.FileName}";
                string savePath = $"{Request.PhysicalApplicationPath}{fileName}";
                System.IO.File.WriteAllBytes(savePath, binaryReader.ReadBytes(uploadImage.ContentLength));
                
            }
            return RedirectToAction("Index");
        }

        public ActionResult DeletePictures(string filePath)
        {
            string galeryPath = $"{Request.PhysicalApplicationPath}Content/images/Gallery/";
            
                System.IO.File.Delete($"{Request.PhysicalApplicationPath}{filePath}"); 
            
            return RedirectToAction("Index");
        }
    }
}


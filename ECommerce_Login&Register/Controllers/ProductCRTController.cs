using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using ECommerce_Login_Register.Models;
using ECommerce_Login_Register.Models.Data;

namespace ECommerce_Login_Register.Controllers
{
    public class ProductCRTController : Controller
    {
        private DataContext db = new DataContext();

        // GET: ProductCRT
        public ActionResult Index()
        {
      
            return View(db.Products.Include("Cat").ToList());
        }
        public ActionResult Enter()
        {
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Product product, HttpPostedFileBase ResimURL)
        {
            if (ModelState.IsValid)
            {
                if (ResimURL != null)
                {
                    WebImage img = new WebImage(ResimURL.InputStream);
                    FileInfo imgInfo = new FileInfo(ResimURL.FileName);

                    string imgname = Guid.NewGuid().ToString() + imgInfo.Extension;
                    img.Save("~/Images/Products/" + imgname);
                    product.Foto = "/Images/Products/" + imgname;
                }
                
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: ProductCRT/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(Product product, int? id, HttpPostedFileBase ResimURL)
        {
            var s = db.Products.Where(x => x.BookId == id).SingleOrDefault();
            if (ModelState.IsValid)
            {
                if (ResimURL != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(s.Foto)))
                    {
                        System.IO.File.Delete(Server.MapPath(s.Foto));
                    }
                    WebImage img = new WebImage(ResimURL.InputStream);
                    FileInfo imgInfo = new FileInfo(ResimURL.FileName);
                    string imgname = Guid.NewGuid().ToString() + imgInfo.Extension;
                    img.Save("~/Images/Products/" + imgname);
                    product.Foto = "/Images/Products/" + imgname;
                }
                s.Foto = product.Foto;
                s.BookName = product.BookName;
                s.BookPrice = product.BookPrice;
                s.BookAuthor = product.BookAuthor;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        public ActionResult Delete(int? id)
        {
            var s = db.Products.Where(x => x.BookId == id).SingleOrDefault();
            //if (System.IO.File.Exists(Server.MapPath(s.Foto)))
            //{
            //    System.IO.File.Delete(Server.MapPath(s.Foto));
            //}
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}

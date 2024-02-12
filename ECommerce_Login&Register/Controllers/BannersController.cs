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
using static System.Data.Entity.Infrastructure.Design.Executor;

namespace ECommerce_Login_Register.Controllers
{
    public class BannersController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Banners
        public ActionResult Index()
        {
            return View(db.Banners.ToList());
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Banner banner,HttpPostedFileBase ResimURL)
        {
            if (ModelState.IsValid)
            {
                if (ResimURL != null)
                {
                    WebImage img = new WebImage(ResimURL.InputStream);
                    FileInfo imgInfo = new FileInfo(ResimURL.FileName);

                    string imgname = Guid.NewGuid().ToString() + imgInfo.Extension;
                    img.Save("~/Images/Banner/" + imgname);
                    banner.Foto = "/Images/Banner/" + imgname;
                }
                db.Banners.Add(banner);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(banner);
        }

        // GET: Banners/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Banner banner = db.Banners.Find(id);
            if (banner == null)
            {
                return HttpNotFound();
            }
            return View(banner);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]

        public ActionResult Edit(Banner banner,HttpPostedFileBase ResimURL,int? id)
        {
            var s = db.Banners.Where(x => x.Id == id).SingleOrDefault();
        
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
                    img.Save("~/Images/Banner/" + imgname);
                    banner.Foto = "/Images/Banner/" + imgname;
                }
                s.Title = banner.Title;
                s.Foto = banner.Foto;
                s.Text = banner.Text;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(banner);
        }

        // GET: Banners/Delete/5
        public ActionResult Delete(int? id)
        {
            var s = db.Banners.Where(x => x.Id == id).SingleOrDefault();
            if (System.IO.File.Exists(Server.MapPath(s.Foto)))
            {
                System.IO.File.Delete(Server.MapPath(s.Foto));
            }
            Banner banner = db.Banners.Find(id);
            db.Banners.Remove(banner);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

       
    }
}

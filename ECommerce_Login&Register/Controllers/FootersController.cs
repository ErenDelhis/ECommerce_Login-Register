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
    public class FootersController : Controller
    {
        private DataContext db = new DataContext();

        public ActionResult Index()
        {
            return View(db.Foots.ToList());
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Footer footer,HttpPostedFileBase ResimURL,int? id)
        {
            var s = db.Foots.Where(x=> x.Id == id).SingleOrDefault();
            if (ModelState.IsValid)
            {
                if (ResimURL != null)
                {
                    WebImage img = new WebImage(ResimURL.InputStream);
                    FileInfo imgInfo = new FileInfo(ResimURL.FileName);
                    string imgname = Guid.NewGuid().ToString() + imgInfo.Extension;
                    img.Save("~/Images/Footer/" + imgname);
                    footer.Logo = "/Images/Footer/" + imgname;
                }
                db.Foots.Add(footer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(footer);
        }

        // GET: Footers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Footer footer = db.Foots.Find(id);
            if (footer == null)
            {
                return HttpNotFound();
            }
            return View(footer);
        }

    
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(Footer footer, HttpPostedFileBase ResimURL, int? id)
        {
            var s = db.Foots.Where(x => x.Id == id).SingleOrDefault();
            if (ModelState.IsValid)
            {
                if (ResimURL != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(s.Logo)))
                    {
                        System.IO.File.Delete(Server.MapPath(s.Logo));
                    }
                    WebImage img = new WebImage(ResimURL.InputStream);
                    FileInfo imgInfo = new FileInfo(ResimURL.FileName);
                    string imgname = Guid.NewGuid().ToString() + imgInfo.Extension;
                    img.Save("~/Images/Footer/" + imgname);
                    footer.Logo = "/Images/Footer/" + imgname;
                }
                s.Text = footer.Text;
                s.Logo = footer.Logo;



                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(footer);
        }

        // GET: Footers/Delete/5
        public ActionResult Delete(int? id)
        {
            Footer footer = db.Foots.Find(id);
            db.Foots.Remove(footer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult LinkCreate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult LinkCreate(Footer footer)
        {
            if (ModelState.IsValid)
            {
                db.Foots.Add(footer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(footer);
        }

        // GET: Footers/Edit/5
        public ActionResult LinkEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Footer footer = db.Foots.Find(id);
            if (footer == null)
            {
                return HttpNotFound();
            }
            return View(footer);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult LinkEdit(Footer footer, int? id)
        {
            var s = db.Foots.Where(x => x.Id == id).SingleOrDefault();
            if (ModelState.IsValid)
            {
                s.LinkTitle = footer.LinkTitle;
                s.Link = footer.Link;
                s.LinkName = footer.LinkName;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(footer);
        }
    }
}

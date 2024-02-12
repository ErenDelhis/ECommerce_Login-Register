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
    public class AdminsController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Admins
        public ActionResult Index()
        {
            return View(db.Admins.ToList());
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(Admin admin, int? id, HttpPostedFileBase ResimURL)
        {
            var s = db.Admins.Where(x => x.Id == id).SingleOrDefault();

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
                    img.Save("~/Images/Admins/" + imgname);
                    admin.Foto = "/Images/Admins/" + imgname;
                }

                s.Foto = admin.Foto;
                s.Mail = admin.Mail;
                s.AdminName = admin.AdminName;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(admin);
        }
        public ActionResult Delete(int? id)
        {
            var s = db.Admins.Where(x => x.Id == id).SingleOrDefault();

            if (System.IO.File.Exists(Server.MapPath(s.Foto)))
            {
                System.IO.File.Delete(Server.MapPath(s.Foto));
            }
            Admin admin = db.Admins.Find(id);
            db.Admins.Remove(admin);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}

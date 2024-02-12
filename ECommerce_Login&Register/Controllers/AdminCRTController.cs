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
using System.Web.UI.WebControls;
using ECommerce_Login_Register.Models;
using ECommerce_Login_Register.Models.Data;

namespace ECommerce_Login_Register.Controllers
{
    public class AdminCRTController : Controller
    {
        private DataContext db = new DataContext();

        // GET: AdminCRT
        [HttpGet]
        public ActionResult Index()
        {
            return View(db.Admins.ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Admin admin,HttpPostedFileBase ResimURL)
        {

            if (ModelState.IsValid)
            {
                if (ResimURL != null)
                {
                    WebImage img = new WebImage(ResimURL.InputStream);
                    FileInfo imgInfo = new FileInfo(ResimURL.FileName);

                    string imgname = Guid.NewGuid().ToString() + imgInfo.Extension;
                    img.Save("~/Images/Admins/" + imgname);
                    admin.Foto = "/Images/Admins/" + imgname;
                }
                db.Admins.Add(admin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(admin);
        }

        // GET: AdminCRT/Edit/5
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

            var Update = db.Admins.Where(x => x.Id == id).SingleOrDefault();
           
            if (ModelState.IsValid)
            {
                if (ResimURL != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(Update.Foto)))
                    {
                        System.IO.File.Delete(Server.MapPath(Update.Foto));
                    }
                    WebImage img = new WebImage(ResimURL.InputStream);
                    FileInfo imgInfo = new FileInfo(ResimURL.FileName);
                    string imgname = Guid.NewGuid().ToString() + imgInfo.Extension;
                    img.Save("~/Images/Admins/" + imgname);
                    admin.Foto = "/Images/Admins/" + imgname;
                }
                Update.AdminName = admin.AdminName;
                Update.Mail = admin.Mail;
                Update.Role = admin.Role;
                Update.Foto = admin.Foto;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(admin);
        }

        public ActionResult Delete(int? id)
        {
            var Update = db.Admins.Where(x => x.Id == id).SingleOrDefault();
            if (System.IO.File.Exists(Server.MapPath(Update.Foto)))
            {
                System.IO.File.Delete(Server.MapPath(Update.Foto));
            }
            Admin admin = db.Admins.Find(id);
            db.Admins.Remove(admin);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Enter()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Admin admin)
        {
            var login = db.Admins.Where(x => x.Mail == admin.Mail).SingleOrDefault();

            if (login.AdminName == admin.AdminName && login.Mail == admin.Mail)
            {

                Session["id"] = login.Id;
                Session["AdminNames"] = login.AdminName;
                Session["Mails"] = login.Mail;
                Session["Roles"] = login.Role;
                Session["Fotos"] = login.Foto;
                return RedirectToAction("Enter", "AdminCRT");
            }
            return View(admin);
        }
        public ActionResult Show()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Show(Admin admin)
        {

            Session["Mails"] = admin.Mail;
            return RedirectToAction("Show", "AdminCRT");

        }

        public ActionResult Logout()
        {

            Session["id"] = null;
            Session["AdminNames"] = null;
            Session["Mails"] = null;
            Session["Roles"] = null;
            Session.Abandon();
            return RedirectToAction("Login", "AdminCRT");

        }
    }
}

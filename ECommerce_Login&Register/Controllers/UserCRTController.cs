using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using ECommerce_Login_Register.Models;
using ECommerce_Login_Register.Models.Data;
using Microsoft.Ajax.Utilities;

namespace ECommerce_Login_Register.Controllers
{
    public class UserCRTController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Users
        public ActionResult Index()
        {
            return View(db.Users.ToList());
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
        public ActionResult Create(User user,HttpPostedFileBase ResimURL)
        {
            if (ModelState.IsValid)
            {
                if (ResimURL != null)
                {
                    WebImage img = new WebImage(ResimURL.InputStream);
                    FileInfo imgInfo = new FileInfo(ResimURL.FileName);

                    string imgname = Guid.NewGuid().ToString() + imgInfo.Extension;
                    img.Save("~/Images/Users/" + imgname);
                    user.Foto = "/Images/Users/" + imgname;
                }
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]

        public ActionResult Edit(User user, int id,HttpPostedFileBase ResimURL)
        {
            var s = db.Users.Where(x => x.UserId == user.UserId).SingleOrDefault();
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
                    img.Save("~/Images/Users/" + imgname);
                    user.Foto = "/Images/Users/" + imgname;
                }
                s.UserName = user.UserName;
                s.Password = user.Password;
                s.UserMail = user.UserMail;
                s.Foto = user.Foto;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }


        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            var s = db.Users.Where(x => x.UserId == id).SingleOrDefault();
            if (System.IO.File.Exists(Server.MapPath(s.Foto)))
            {
                System.IO.File.Delete(Server.MapPath(s.Foto));
            }
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();

            if (Session["id"] == null)
            {
                return RedirectToAction("Home","Index");

            }
            else
            {
                return RedirectToAction("Index");

            }
        }


        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index","Home");
            }

            return View();
        }
        //public ActionResult Login()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult Login(User user)
        //{
        //    var login = db.Users.Where(x => x.UserMail == user.UserMail).SingleOrDefault();
        //    if (login.UserName == user.UserName && login.Password == user.Password && login.UserMail == user.UserMail)
        //    {
        //        Session["Userid"]   = login.UserId;
        //        Session["Usernames"] = login.UserName;
        //        Session["Passwords"] = login.Password;
        //        Session["UserMails"] = login.UserMail;
        //        Session["UserFoto"] = login.Foto;
        //        return RedirectToAction("Enter", "UserCRT");
        //    }
        //    else
        //    {
        //        return RedirectToAction("Login", "UserCRT");
        //    }
        //}
       
        public ActionResult Logout()
        {

            Session["Userid"] = null;
            Session["Usernames"] = null;
            Session["Passwords"] = null;
            Session["UserMails"] = null;
            Session["UserFoto"] = null;
            Session.Abandon();
            return RedirectToAction("Index", "Home");

        }

    }
}

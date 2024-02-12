using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using ECommerce_Login_Register.Models;
using ECommerce_Login_Register.Models.Data;

namespace ECommerce_Login_Register.Controllers
{
    public class ClientsController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Clients
        public ActionResult Index()
        {
            return View(db.Clients.ToList());
        }

 
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Client client, HttpPostedFileBase ResimURL)
        {
            if (ModelState.IsValid)
            {
                if (ResimURL != null)
                {
                    WebImage img = new WebImage(ResimURL.InputStream);
                    FileInfo imgInfo = new FileInfo(ResimURL.FileName);

                    string imgname = Guid.NewGuid().ToString() + imgInfo.Extension;
                    img.Save("~/Images/Clients/" + imgname);
                    client.Logo = "/Images/Clients/" + imgname;
                }
                db.Clients.Add(client);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(client);
        }

        // GET: Clients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(Client client,int? id, HttpPostedFileBase ResimURL)
        {
            var s = db.Clients.Where(x => x.Id == id).SingleOrDefault();
         
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
                    img.Save("~/Images/Clients/" + imgname);
                    client.Logo = "/Images/Clients/" + imgname;
                }
                s.Logo = client.Logo;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(client);
        }

        // GET: Clients/Delete/5
        public ActionResult Delete(int? id)
        {
            var s = db.Clients.Where(x => x.Id == id).SingleOrDefault();
            if (System.IO.File.Exists(Server.MapPath(s.Logo)))
            {
                System.IO.File.Delete(Server.MapPath(s.Logo));
            }
            Client client = db.Clients.Find(id);
            db.Clients.Remove(client);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    
    }
}

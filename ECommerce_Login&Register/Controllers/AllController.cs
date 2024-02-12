using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ECommerce_Login_Register.Models;
using ECommerce_Login_Register.Models.Data;

namespace ECommerce_Login_Register.Controllers
{
    public class AllController : Controller
    {
        private DataContext db = new DataContext();
        public ActionResult Index()
        {
            var alls = db.Alls.Include(a => a.Cat);
            return View(alls.ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(All all)
        {
            if (ModelState.IsValid)
            {
                db.Alls.Add(all);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(all);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            All all = db.Alls.Find(id);
            if (all == null)
            {
                return HttpNotFound();
            }
            return View(all);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(All all)
        {
            if (ModelState.IsValid)
            {
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(all);
        }
        public ActionResult Delete(int? id)
        {
            All all = db.Alls.Find(id);
            db.Alls.Remove(all);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

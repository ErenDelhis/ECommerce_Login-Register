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
    public class BusinessesController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Businesses
        public ActionResult Index()
        {
            var business = db.Business.Include(b => b.Cat);
            return View(business.ToList());
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Business business)
        {
            if (ModelState.IsValid)
            {
                db.Business.Add(business);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(business);
        }

        // GET: Businesses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Business business = db.Business.Find(id);
            if (business == null)
            {
                return HttpNotFound();
            }
            return View(business);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]

        public ActionResult Edit(int? id, Business business)
        {
            var s = db.Business.Where(x => x.BusinId == id).SingleOrDefault();
            if (ModelState.IsValid)
            {
                s.BusinName = business.BusinName;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(business);
        }

        // GET: Businesses/Delete/5
        public ActionResult Delete(int? id)
        {
            Business business = db.Business.Find(id);
            db.Business.Remove(business);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

       
    }
}

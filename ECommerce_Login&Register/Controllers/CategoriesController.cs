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
    public class CategoriesController : Controller
    {
        private DataContext db = new DataContext();
        public ActionResult Index()
        {
            return View(db.Categors.ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                db.Categors.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }
        // GET: Kopyala/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categors.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(Category category,int? id)
        {
            var s = db.Categors.Where(x => x.CategoryId == id).SingleOrDefault();
            if (ModelState.IsValid)
            {
                s.Tur = category.Tur;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        
            return View(category);
        }
        // GET: Kopyala/Delete/5
        public ActionResult Delete(int? id)
        {
            Category category = db.Categors.Find(id);
            db.Categors.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

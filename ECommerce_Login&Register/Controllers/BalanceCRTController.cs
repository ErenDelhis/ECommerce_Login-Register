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
    public class BalanceCRTController : Controller
    {

        private DataContext db = new DataContext();
        // GET: Balances
        public ActionResult Index()
        {

            var s = db.Balances.Include("Us").ToList();

            return View(s);
        }

        public ActionResult Inc()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(true)]
        public ActionResult Inc(Balance Us,int? id)
        {
            var add = db.Balances.FirstOrDefault(b => b.Balance_Id == id);
            if (ModelState.IsValid)
            {
                // Bütçe varsa miktarını arttır.
                add.Butce += Us.Butce;
                db.Entry(add).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "BalanceCRT");
            }
            return View(Us);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(true)]
        public ActionResult Create(Balance Us)
        {
            if (ModelState.IsValid)
            {
                db.Balances.Add(Us);
                db.SaveChanges();
                return RedirectToAction("Index", "BalanceCRT");
            }
            return View(Us);
        }
        public ActionResult Dec()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(true)]
        public ActionResult Dec(Balance Us, int? id)
        {
            var AddBalance = db.Balances.FirstOrDefault(b => b.Balance_Id == id);

            if (ModelState.IsValid)
            {
                // Bütçe varsa miktarını arttır.
                AddBalance.Butce -= Us.Butce;
                db.Entry(AddBalance).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "BalanceCRT");
            }
            return View(Us);
        }
        // GET: Balances/Delete/5
        public ActionResult Delete(int? id)
        {
            Balance balance = db.Balances.Find(id);
            db.Balances.Remove(balance);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}

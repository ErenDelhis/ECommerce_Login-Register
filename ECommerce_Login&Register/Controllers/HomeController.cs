using ECommerce_Login_Register.Models;
using ECommerce_Login_Register.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerce_Login_Register.Controllers
{
    public class HomeController : Controller
    {
        DataContext db = new DataContext();
        public ActionResult Index()
        {
            var categories = db.Categors.ToList();
            return View(categories);
        }
        public PartialViewResult Banner()
        {
            return PartialView(db.Banners.ToList());
        }
        public PartialViewResult Footer()
        {
            return PartialView(db.Foots.ToList());
        }
        public PartialViewResult Categori()
        {
            return PartialView(db.Categors.SingleOrDefault());
        }
        public PartialViewResult Cat_Book(int? id)
        {
            var b = db.Products.Include("Cat").OrderByDescending(x => x.BookId).Where(x => x.Cat.CategoryId == id).ToList();

            return PartialView(b);
        }
        public PartialViewResult Book(int? categoryId)
        {
            IQueryable<Product> filteredBooks;

            if (categoryId.HasValue)
            {
                // Sadece seçilen kategoriye ait kitapları getir
                filteredBooks = db.Products.Include("Cat").OrderByDescending(x => x.BookId)
                    .Where(x => x.Cat.CategoryId == categoryId);
            }
            else
            {
                // Kategori belirtilmemişse tüm kitapları getir
                filteredBooks = db.Products.Include("Cat").OrderByDescending(x => x.BookId);
            }

            // Sadece kitap listesini döndür
            return PartialView(filteredBooks.ToList());
        }
        public PartialViewResult Client()
        {
            return PartialView(db.Clients.ToList());
        }
        public PartialViewResult Quote()
        {
            return PartialView(db.Quotes.ToList());
        }
        public ActionResult IndexLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult IndexLogin(User user)
        {
            var login = db.Users.Where(x => x.UserMail == user.UserMail).SingleOrDefault();

            if (login.UserName == user.UserName && login.Password == user.Password && login.UserMail == user.UserMail)
            {
                Session["Userid"] = login.UserId;
                Session["Usernames"] = login.UserName;
                Session["Passwords"] = login.Password;
                Session["UserMails"] = login.UserMail;
                Session["UserFoto"] = login.Foto;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Login", "UserCRT");
            }
        }
        [HttpGet]
        public ActionResult CheckBalance()
        {
            if (Session["Userid"] == null)
            {
                // Kullanıcı oturum açmamışsa, hata döndür
                return HttpNotFound("Giriş yapmadınız.");
            }
            else
            {
                try
                {
                    // Kullanıcının bakiyesini al
                    int userId = (int)Session["Userid"];
                    var balance = db.Balances.FirstOrDefault(b => b.UserId == userId);
                    if (balance != null)
                    {
                        // Bakiyeyi JSON olarak döndür
                        return Json(balance.Butce, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        // Kullanıcının bakiyesi bulunamadı, hata döndür
                        return HttpNotFound("Kullanıcının bakiyesi bulunamadı.");
                    }
                }
                catch (Exception ex)
                {
                    // Hata oluştu, hata döndür
                    return HttpNotFound("Hata: " + ex.Message);
                }
            }
        }
        [HttpPost]
        public ActionResult ReduceBalance(decimal amount)
        {
            if (Session["Userid"] == null)
            {
                // Kullanıcı giriş yapmamış, uyarı mesajı göster ve giriş yapma sayfasına yönlendir
                return Content("<script>alert('Giriş yapınız.'); window.location.href='/Home/IndexLogin';</script>");
            }
            else
            {
                try
                {
                    // Kullanıcının bakiyesinden belirtilen tutarı düşür
                    int userId = (int)Session["Userid"];
                    var balance = db.Balances.FirstOrDefault(b => b.UserId == userId);
                    if (balance != null)
                    {
                        if (balance.Butce >= amount)
                        {
                            balance.ReduceBalance(amount);
                            db.SaveChanges();
                            return Json(new { success = true });
                        }
                        else
                        {
                            // Kullanıcının bakiyesi yetersiz, uyarı mesajı göster
                            return Content("<script>alert('Yetersiz bakiye!');</script>");
                        }
                    }
                    else
                    {
                        // Kullanıcının bakiyesi bulunamadı, uyarı mesajı göster
                        return Content("<script>alert('Kullanıcının bakiyesi bulunamadı.');</script>");
                    }
                }
                catch (Exception ex)
                {
                    // Hata oluştu, uyarı mesajı göster
                    return Content($"<script>alert('Hata: {ex.Message}');</script>");
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ECommerce_Login_Register.Models.Data
{
    public class DataContext : DbContext
    {
        public DataContext():base("ECommerce")
        {
            
        }
        public DbSet<Admin> Admins { get; set; }    
        public DbSet<User> Users { get; set; }
        public DbSet<Footer> Foots { get; set; }
        public DbSet<CartItem> Cart { get; set; }
        public DbSet<Balance> Balances { get; set; }
        public DbSet<Product> Products { get; set; }    
        public DbSet<Client> Clients { get; set; }    
        public DbSet<Quote> Quotes { get; set; }    
        public DbSet<Category> Categors { get; set; }    
        public DbSet<Banner> Banners { get; set; }
        public DbSet<Adventure> Adventures { get; set; }
        public DbSet<Fictional> Fictionals { get; set; }
        public DbSet<Romantic> Romantics { get; set; }
        public DbSet<Technology> Technologies { get; set; }
        public DbSet<Business> Business { get; set; }
        public DbSet<All> Alls { get; set; }



    }
}
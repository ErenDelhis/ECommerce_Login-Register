using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ECommerce_Login_Register.Models
{
    public class Product
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookId { get; set; }
        public string BookName { get; set; }
        public string BookAuthor { get; set; }
        public decimal BookPrice { get; set; }
        public string Foto { get; set; }

        // Bir ürünün bir kategorisi vardır.
        public int? CategoryId { get; set; }
        public  Category Cat { get; set; }

    }
}
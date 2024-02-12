using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ECommerce_Login_Register.Models
{
    public class Category
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; }
        public string Tur { get; set; }
        // Bir kategorinin birden fazla ürünü olabilir.
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Business> Businesses { get; set; }
        public virtual ICollection<Adventure> Adven { get; set; }
        public virtual ICollection<Fictional> Fic { get; set; }
        public virtual ICollection<Technology> Tech { get; set; }
        public virtual ICollection<Romantic> Rom { get; set; }
        public virtual ICollection<All> Al { get; set; }




        /*
            public int? BusinId { get; set; }
        public Business Businesses { get; set; }

        public int? TechId { get; set; }
        public Technology Thec { get; set; }

        public int? RomId { get; set; }
        public Romantic Rom { get; set; }

        public int? AllId { get; set; }
        public All Al { get; set; }

        public int? AdvenId { get; set; }
        public Adventure Adven { get; set; }

        public int? FictId { get; set; }
        public Fictional Fic { get; set; }
         
         */

    }
}
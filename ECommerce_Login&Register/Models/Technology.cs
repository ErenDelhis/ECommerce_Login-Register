using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ECommerce_Login_Register.Models
{
    public class Technology
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TechId { get; set; }
        public string TachName { get; set; }
        public int? CategoryId { get; set; }
        public Category Cat { get; set; }

        //public virtual ICollection<Category> Cat { get; set; }
        
    }
}
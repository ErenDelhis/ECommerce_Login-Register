using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ECommerce_Login_Register.Models
{
    public class Business
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BusinId { get; set; }
        public string BusinName { get; set; }
        public int? CategoryId { get; set; }
        public Category Cat { get; set; }
        //public virtual ICollection<Category> Cat { get; set; }


    }
}
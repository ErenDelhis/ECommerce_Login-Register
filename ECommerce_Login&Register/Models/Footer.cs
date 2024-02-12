using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ECommerce_Login_Register.Models
{
    public class Footer
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Logo { get; set; }
        public string Text { get; set; }
        public string LinkTitle { get; set; }
        public string LinkName { get; set; }
        public string Link { get; set; }

    }
}
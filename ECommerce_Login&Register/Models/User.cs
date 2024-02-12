using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ECommerce_Login_Register.Models
{
    public class User
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        [StringLength(30,ErrorMessage = "En fazla 30 karakter girebilirsiniz.")]
        [Required]
        public string UserName { get; set; }
        [StringLength(50, ErrorMessage = "En fazla 30 karakter girebilirsiniz.")]
        [Required]
        public string Password { get; set; }
        [StringLength(50, ErrorMessage = "En fazla 50 karakter girebilirsiniz.")]
        [Required]
        public string UserMail { get; set; }
        public string Foto { get; set; }

        public ICollection<Balance> Balan { get; set; }
    }
}
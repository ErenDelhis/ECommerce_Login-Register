using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ECommerce_Login_Register.Models
{
    public class Balance// Adres
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Balance_Id { get; set; }
        public decimal Butce { get; set; }





        public void ReduceBalance(decimal amount)
        {
            if (Butce >= amount)
            {
                Butce -= amount;
            }
            else
            {
                throw new Exception("Yetersiz bakiye!");
            }
        }



        #region İlişkili Tablo
        public int? UserId { get; set; }
        public User Us { get; set; }
        #endregion
       
    }
}
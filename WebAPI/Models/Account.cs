using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public class Account
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; private set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal Balance { get; set; }
        public int UserId { get; set; }

        public Account()
        {
            CreatedAt = DateTime.Now;
            Balance = 0;
        }
    }
}
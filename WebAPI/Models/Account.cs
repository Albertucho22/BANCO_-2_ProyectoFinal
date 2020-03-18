using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public class Account
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime CreatedAt { get; private set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Balance { get; set; }

        [Required]
        public int ClientId { get; set; }

        public Account()
        {
            CreatedAt = DateTime.Now;
            Balance = 0;
        }
    }
}
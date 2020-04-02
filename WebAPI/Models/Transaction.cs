using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
  public abstract class Transaction
  {

    [Key]
    public int Id { get; set; }

    [Required]
    public DateTime CreatedAt
    {
      get
      {
        return DateTime.Now;
      }
      private set { }
    }
    // public int IssuingAccount { get; set; }
    // public int DestinationAccount { get; set; }
    // public string Type { get; set; }
    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal Amount { get; set; }

    [Required]
    [StringLength(100)]
    public string Information { get; set; }

    [Required]
    public int AccountId { get; set; }

    public string Discriminator { get; set; }
  }
}
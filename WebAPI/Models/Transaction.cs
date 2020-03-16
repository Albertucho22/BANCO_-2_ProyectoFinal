using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
  public class Transaction
  {
    public int Id { get; set; }
    public DateTime CreatedAt { get; private set; }
    // public int IssuingAccount { get; set; }
    // public int DestinationAccount { get; set; }
    // public string Type { get; set; }
    [Column(TypeName = "decimal(10,2)")]
    public decimal Amount { get; set; }
    public string Information { get; set; }

    public Transaction()
    {
        CreatedAt = DateTime.Now;
    }
  }
}
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeesDashboard.Models.API {
  public class Transaction {

    [Key]
    public int Id { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }
    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal Amount { get; set; }

    [Required]
    [StringLength(100)]
    public string Information { get; set; }

    [Required]
    public int AccountId { get; set; }

    [Display(Name = "Type")]
    public string Discriminator { get; set; }
  }
}
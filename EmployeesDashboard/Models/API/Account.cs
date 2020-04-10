using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeesDashboard.Models.API {
  public class Account {
    [Key]
    public int Id { get; set; }

    [Required]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime CreatedAt { get; set; }

    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal Balance { get; set; }

    [Required]
    public int ClientId { get; set; }

    // [ForeignKey("AccountId")]
    // public virtual List<Loan> Loans { get; set; }

    // [ForeignKey("AccountId")]
    // public virtual List<Transaction> Transactions { get; set; }
  }
}
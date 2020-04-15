using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.VisualBasic;

namespace EmployeesDashboard.Models.API {
  public class Loan {
    public int Id { get; set; }

    [Display(Name = "Interest Rate (Yearly)")]
    public float YearlyInterestRate { get; set; }

    [Display(Name = "Duration (Months)")]
    public int DurationInMonths { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    [Display(Name = "Total Amount")]
    public decimal TotalAmount { get; set; }

    [Display(Name = "Account")]
    public int AccountId { get; set; }

    public string Information { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    [Display(Name = "Monthly Payment")]
    public decimal MonthlyPayment { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    [Display(Name="Remaining Amount")]
    public decimal RemainingAmount { get; set; }
  }
}

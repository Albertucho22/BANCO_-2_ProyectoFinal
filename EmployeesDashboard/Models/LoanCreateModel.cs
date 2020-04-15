using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeesDashboard.Models {
  public class LoanCreateModel {
    [Display(Name = "Interest Rate (Yearly)")]
    public float YearlyInterestRate { get; set; }

    [Display(Name = "Duration (Months)")]
    public int DurationInMonths { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    [Display(Name = "Total Amount")]
    public decimal TotalAmount { get; set; }

    [Display(Name = "Account ID")]
    public int AccountId { get; set; }

    [Display(Name = "Information")]
    public string Information { get; set; }
  }
}

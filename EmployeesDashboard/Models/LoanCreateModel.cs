using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeesDashboard.Models {
  public class LoanCreateModel {
    public float YearlyInterestRate { get; set; }
    public int DurationInMonths { get; set; }
    [Column(TypeName = "decimal(10,2)")]
    public decimal TotalAmount { get; set; }
    public int AccountId { get; set; }
    public string Information { get; set; }
  }
}

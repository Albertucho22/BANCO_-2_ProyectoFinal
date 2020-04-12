using System.ComponentModel.DataAnnotations;

namespace EmployeesDashboard.Models {
  public class AccountCreateModel {
    [Required]
    public int ClientId { get; set; }
  }
}
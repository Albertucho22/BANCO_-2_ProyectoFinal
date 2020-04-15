using System.ComponentModel.DataAnnotations;

namespace EmployeesDashboard.Models.API {
  public class LocalTransfer : Transaction {
    [Display(Name = "Receiver Account")]
    public int ReceiverAccountId { get; set; }
  }
}
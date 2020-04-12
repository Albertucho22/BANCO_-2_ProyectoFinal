using System.Collections.Generic;

namespace EmployeesDashboard.Models {
  public class EmployeeViewModel {
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public IList<string> Roles { get; set; }
  }
}
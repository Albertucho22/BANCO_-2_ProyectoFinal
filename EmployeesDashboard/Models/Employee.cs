using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace EmployeesDashboard.Models {
  public class Employee : IdentityUser {
    [Required]
    [StringLength(50)]
    [Display(Name = "First Name")]
    public string FirstName { get; set; }

    [Required]
    [StringLength(50)]
    [Display(Name = "Last Name")]
    public string LastName { get; set; }
  }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeesDashboard.Models {
  public class ClientCreateModel {
    [Required]
    [StringLength(50)]
    [Display(Name = "First Name")]

    public string FirstName { get; set; }

    [Required]
    [StringLength(50)]
    [Display(Name = "Last Name")]
    public string LastName { get; set; }

    [Required]
    [StringLength(50)]
    public string UserName { get; set; }

    [Required]
    [StringLength(50)]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required]
    [EmailAddress]
    [StringLength(50)]
    public string Email { get; set; }

    [Required]
    [StringLength(50)]
    public string NationalId { get; set; }
  }
}
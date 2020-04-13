using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models {
  public class LoanPayment : Transaction {
      [Required]
      public int LoanId { get; set; }
  }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class LoanPayment : Transaction
    {

        [Required]
        [StringLength(50)]
        public int LoanPaymentId { get; set; }

        /*[Column(TypeName = "decimal(10,2)")]
        public decimal TotalAmount { get; set; }*/

        [Column(TypeName = "decimal(10,2)")]
        public decimal PaidAmount { get; set; }


    }
}

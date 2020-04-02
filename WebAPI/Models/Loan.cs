using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.VisualBasic;

namespace WebAPI.Models {
  public class Loan {
    public int Id { get; set; }

    public float YearlyInterestRate { get; set; }
    public int DurationInMonths { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal TotalAmount { get; set; }
    public int AccountId { get; set; }
    public string Information { get; set; }
    public double MonthlyPayment {
      get {
        return PMT(YearlyInterestRate, DurationInMonths, decimal.ToDouble(TotalAmount));
      }
      private set { }
    }

    [Column(TypeName = "decimal(10,2)")]
    public decimal RemainingAmount {
      get {
        // totalAmount - all payments
        return TotalAmount;
      }
      private set { }
    }

    private double PMT(double yearlyInterestRate, int totalNumberOfMonths, double loanAmount) {
      double rate = yearlyInterestRate / 100 / 12;
      double denominator = Math.Pow((1 + rate), totalNumberOfMonths) - 1;
      return (rate + (rate / denominator)) * loanAmount;
    }
  }
}

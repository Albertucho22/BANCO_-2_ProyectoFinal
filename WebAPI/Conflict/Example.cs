using System;
using WebAPI.Models;

namespace WebAPI.Conflict {
  public class Example {
    public Deposit Deposit { get; set; }

    private string CouponCode { get; set; }

    public byte Identification { get; set; }

    public Example()
    {
        CouponCode = "ju78ik";
        Deposit = new Deposit();
    }
  }
}
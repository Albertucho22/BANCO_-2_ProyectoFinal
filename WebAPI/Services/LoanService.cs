using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Services {
  public class LoanService {
    private readonly Core2DbContext _context;

    public LoanService(Core2DbContext context) {
      _context = context;
    }

    public async Task<List<Loan>> Get() =>
      await _context.Loans.ToListAsync();

    public async Task<Loan> Get(int id) {
      var loan = await _context.Loans.FindAsync(id);
      if (loan == null) throw new Exception("No Loan found with given Id.");

      return loan;
    }

    public async Task<Loan> Create(Loan loan) {
      loan.RemainingAmount = loan.TotalAmount;
      _context.Loans.Add(loan);
      await _context.SaveChangesAsync();
      return loan;
    }

    public async Task<Loan> UpdateRemainingAmount(int id, decimal loanPaymentAmount) {
      var loan = await _context.Loans.FindAsync(id);
      if (loan == null) throw new Exception("No Loan found with given Id.");

      decimal updatedRemainingAmount = loan.RemainingAmount - loanPaymentAmount;
      if (updatedRemainingAmount < 0) {
        if (loan.RemainingAmount == 0) throw new Exception("Loan is already paid. No further action needed.");
        throw new Exception($"Loan is due less than the given Loan Payment. Please pay ${loan.RemainingAmount} or less.");
      }

      loan.RemainingAmount = updatedRemainingAmount;

      _context.Loans.Update(loan);

      try {
        await _context.SaveChangesAsync();
      } catch (Exception) {
        throw;
      }
      return loan;
    }
  }
}
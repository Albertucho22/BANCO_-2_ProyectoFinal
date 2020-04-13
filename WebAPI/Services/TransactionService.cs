using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Services {
  public class TransactionService {
    private readonly Core2DbContext _context;

    public TransactionService(Core2DbContext context) {
      _context = context;
    }

    public async Task<List<Deposit>> GetDeposits() =>
      await _context.Deposits.ToListAsync();

    public async Task<Deposit> GetDeposits(int id) {
      var deposit = await _context.Deposits.FindAsync(id);
      if (deposit == null) throw new Exception("No Deposit found with given Id.");

      return deposit;
    }

    public async Task<List<Withdrawal>> GetWithdrawals() =>
      await _context.Withdrawals.ToListAsync();

    public async Task<Withdrawal> GetWithdrawals(int id) {
      var withdrawal = await _context.Withdrawals.FindAsync(id);
      if (withdrawal == null) throw new Exception("No Withdrawal found with given Id.");

      return withdrawal;
    }

        internal Task<List<LoanPayment>> GetLoanPayments()
        {
            throw new NotImplementedException();
        }

        public async Task<Deposit> Create(Deposit deposit) {
      _context.Deposits.Add(deposit);
      await _context.SaveChangesAsync();
      return deposit;
    }
    public async Task<Withdrawal> Create(Withdrawal withdrawal) {
      _context.Withdrawals.Add(withdrawal);
      await _context.SaveChangesAsync();
      return withdrawal;
    }
  }
}
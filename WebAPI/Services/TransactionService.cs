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

<<<<<<< HEAD
        internal Task<List<LoanPayment>> GetLoanPayments()
        {
            throw new NotImplementedException();
        }

        public async Task<Deposit> Create(Deposit deposit) {
=======
    public async Task<List<LocalTransfer>> GetLocalTransfers() =>
      await _context.LocalTransfers.ToListAsync();

    public async Task<LocalTransfer> GetLocalTransfers(int id) {
      var localTransfer = await _context.LocalTransfers.FindAsync(id);
      if (localTransfer == null) throw new Exception("No Local Transfer found with given Id.");

      return localTransfer;
    }

    public async Task<List<LoanPayment>> GetLoanPayments() =>
      await _context.LoanPayments.ToListAsync();

    public async Task<LoanPayment> GetLoanPayments(int id) {
      var loanPayment = await _context.LoanPayments.FindAsync(id);
      if (loanPayment == null) throw new Exception("No Loan Payment found with given Id.");

      return loanPayment;
    }

    public async Task<Deposit> Create(Deposit deposit) {
>>>>>>> ab4a47c7d6a18c6fef13e2b7747a787c3f0a447f
      _context.Deposits.Add(deposit);
      await _context.SaveChangesAsync();
      return deposit;
    }
    public async Task<Withdrawal> Create(Withdrawal withdrawal) {
      _context.Withdrawals.Add(withdrawal);
      await _context.SaveChangesAsync();
      return withdrawal;
    }

    public async Task<LocalTransfer> Create(LocalTransfer localTransfer) {
      _context.LocalTransfers.Add(localTransfer);
      await _context.SaveChangesAsync();
      return localTransfer;
    }

    public async Task<LoanPayment> Create(LoanPayment loanPayment) {
      _context.LoanPayments.Add(loanPayment);
      await _context.SaveChangesAsync();
      return loanPayment;
    }
  }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;
using WebAPI.Services;

namespace Controllers {
  [Route("api/[controller]")]
  [ApiController]
  public class LoanPaymentsController : ControllerBase {
    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    private readonly TransactionService _transactionService;

    public LoanPaymentsController(TransactionService transactionService) {
      _transactionService = transactionService;
    }

    // GET: api/LoanPayments
    [HttpGet]
    public async Task<ActionResult<List<LoanPayment>>> GetLoanPayments() {
      try {
        List<LoanPayment> loanPayments = await _transactionService.GetLoanPayments();
        log.Info($"Get {loanPayments.Count} loan payments");
        return Ok(loanPayments);
      } catch (Exception e) {
        log.Error(e);
        return BadRequest(new {
          error = new {
            message = e.Message
          }
        });
      }
    }

    // GET: api/LoanPayments/5
    [HttpGet("{id}")]
    public async Task<ActionResult<LoanPayment>> GetLoanPayment(int id) {
      try {
        var loanPayments = await _transactionService.GetLoanPayments(id);
        if (loanPayments == null) {
          log.Warn($"Loan Payment {id} not found");
          return NotFound();
        }
        log.Info($"Get loan payment {id}");
        return loanPayments;
      } catch (System.Exception e) {
        log.Error(e);
        return BadRequest(new {
          error = new {
            message = e.Message
          }
        });
      }
    }

    /// PUT: api/LoanPayments/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for
    // more details see https://aka.ms/RazorPagesCRUD.
    [HttpPut("{id}")]
    public IActionResult PutLoanPayment() {
      log.Warn("Deny PUT action from /api/Withdrawals");
      return BadRequest(new { error = new { message = "Update operation not permitted on Transactions " } });
    }

    // POST: api/LoanPayments
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for
    // more details see https://aka.ms/RazorPagesCRUD.
    [HttpPost]
    public async Task<ActionResult<LoanPayment>> PostLoanPayment(LoanPayment loanPayment, [FromServices] AccountService _accountService, [FromServices] LoanService _loanService) {
      try {
        var account = await _accountService.Get(loanPayment.AccountId);
        if (account == null) {
          log.Warn($"Account {loanPayment.AccountId} not found.");
          return BadRequest("Account not found."); // use proper error obj
        }

        var loan = await _loanService.Get(loanPayment.LoanId);
        if (loan == null) {
          log.Warn($"Loan {loanPayment.LoanId} not found.");
          return BadRequest("Loan not found.");
        }

        account.UpdateBalance(loanPayment.Amount * -1);
        await _accountService.Update(account.Id, account);

        await _loanService.UpdateRemainingAmount(loan.Id, loanPayment.Amount);

        log.Info($"Loan Payment {loanPayment.Id} has been created.");
        return await _transactionService.Create(loanPayment);
      } catch (System.Exception e) {
        log.Error(e);
        return BadRequest(new {
          error = new {
            message = e.Message
          }
        });
      }
    }

    // DELETE: api/LoanPayments
    [HttpDelete]
    public ActionResult DeleteLoanPayment() {
      log.Warn("Deny DELETE action from /api/LoanPayments");
      return BadRequest(new {
        error = new { message = "Delete operation not permitted on Transactions " }
      });
    }
  }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Controllers {
  [Route("api/[controller]")]
  [ApiController]
  public class LoansController : ControllerBase {
    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    private readonly LoanService _loanService;

    public LoansController(LoanService loanService) {
      _loanService = loanService;
    }

    // GET: api/Loans
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Loan>>> GetLoan() {
      try {
        List<Loan> loans = await _loanService.Get();
        log.Info($"Get {loans.Count} loans");
        return Ok(loans);
      } catch (System.Exception e) {
        log.Error(e);
        return BadRequest(new {
          error = new {
            message = e.Message
          }
        });
      }
    }

    // GET: api/Loans/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Loan>> GetLoan(int id) {
      try {
        var loan = await _loanService.Get(id);

        if (loan == null) {
          log.Warn($"Loan {id} not found");
          return NotFound();
        }

        log.Info($"Get loan {id}");
        return loan;
      } catch (System.Exception e) {
        log.Error(e);
        return BadRequest(new {
          error = new {
            message = e.Message
          }
        });
      }
    }

    // PUT: api/Deposits/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for
    // more details see https://aka.ms/RazorPagesCRUD.
    [HttpPut("{id}")]
    public IActionResult PutLoan() {
      log.Warn("Deny PUT action from /api/Loans");
      return BadRequest(new { error = new { message = "Update operation not permitted on Loans " } });
    }

    // POST: api/Loans
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for
    // more details see https://aka.ms/RazorPagesCRUD.
    [HttpPost]
    public async Task<ActionResult<Loan>> PostLoan(Loan loan, [FromServices] AccountService _accountService) {
      try {
        var account = await _accountService.Get(loan.AccountId);
        if (account == null) {
          log.Warn($"Account {loan.AccountId} not found.");
          return BadRequest("Account not found.");
        }

        Loan createdLoan = await _loanService.Create(loan);
        log.Info($"Loan {createdLoan.Id} has been created.");
        return createdLoan;
      } catch (Exception e) {
        log.Error(e);
        return BadRequest(new { error = new { message = e.Message } });
      }
    }

    // DELETE: api/Loans
    [HttpDelete]
    public ActionResult DeleteLoan() {
      log.Warn("Deny DELETE action from /api/Loans");
      return BadRequest(new {
        error = new { message = "Delete operation not permitted on Loans " }
      });
    }
  }
}

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
  public class WithdrawalsController : ControllerBase {
    private readonly TransactionService _transactionService;

    public WithdrawalsController(TransactionService transactionService) {
      _transactionService = transactionService;
    }

    // GET: api/Withdrawals
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Withdrawal>>> GetWithdrawal() {
      try {
        List<Withdrawal> withdrawals = await _transactionService.GetWithdrawals();
        return Ok(withdrawals);
      } catch (Exception e) {
        return BadRequest(new {
          error = new {
            message = e.Message
          }
        });
      }
    }

    // GET: api/Withdrawals/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Withdrawal>> GetWithdrawal(int id) {
      try {
        var withdrawal = await _transactionService.GetWithdrawals(id);
        if (withdrawal == null) return NotFound();

        return withdrawal;
      } catch (Exception e) {
        return BadRequest(new {
          error = new {message = e.Message}
        });
      }
    }

    // PUT: api/Withdrawals
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for
    // more details see https://aka.ms/RazorPagesCRUD.
    [HttpPut]
    public IActionResult PutWithdrawal() =>
        BadRequest(new { error = new { message = "Update operation not permitted on Transactions " } });

    // POST: api/Withdrawals
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for
    // more details see https://aka.ms/RazorPagesCRUD.
    [HttpPost]
    public async Task<ActionResult<Withdrawal>> PostWithdrawal(Withdrawal withdrawal, [FromServices] AccountService _accountService) {
      try {
        var account = await _accountService.Get(withdrawal.AccountId);
        if (account == null) return BadRequest("Account not found."); // use proper error obj

        account.UpdateBalance(withdrawal.Amount * -1);
        await _accountService.Update(account.Id, account);
        return await _transactionService.Create(withdrawal);
      } catch (Exception e) {
        return BadRequest(new {
          error = new { message = e.Message }
        });
      }
    }

    // DELETE: api/Withdrawals/5
    [HttpDelete("{id}")]
    public ActionResult DeleteWithdrawal() =>
      BadRequest(new {
        error = new { message = "Delete operation not permitted on Transactions " }
      });
  }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Controllers {
  [Route("api/[controller]")]
  [ApiController]
  public class DepositsController : ControllerBase {
    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    private readonly TransactionService _transactionService;

    public DepositsController(TransactionService transactionServie) {
      _transactionService = transactionServie;
    }

    // GET: api/Deposits
    [HttpGet]
    public async Task<ActionResult<List<Deposit>>> GetDeposit() {
      try {
        List<Deposit> deposits = await _transactionService.GetDeposits();
        log.Info($"Get {deposits.Count} deposits");
        return Ok(deposits);
      } catch (Exception e) {
        log.Error(e);
        return BadRequest(new {
          error = new {
            message = e.Message
          }
        });
      }
    }

    // GET: api/Deposits/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Deposit>> GetDeposit(int id) {
      try {
        var deposit = await _transactionService.GetDeposits(id);
        if (deposit == null) {
          log.Warn($"Deposit {id} not found");
          return NotFound();
        }
        log.Info($"Get deposit {id}");
        return deposit;
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
    public IActionResult PutDeposit() {
      log.Warn("Deny PUT action from /api/Deposits");
      return BadRequest(new { error = new { message = "Update operation not permitted on Transactions " } });
    }

    // POST: api/Deposits
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for
    // more details see https://aka.ms/RazorPagesCRUD.
    [HttpPost]
    public async Task<ActionResult<Deposit>> PostDeposit(Deposit deposit, [FromServices] AccountService _accountService) {
      try {
        var account = await _accountService.Get(deposit.AccountId);
        if (account == null) {
          log.Warn($"Account {deposit.AccountId} not found.");
          return BadRequest("Account not found."); // use proper error obj
        }

        account.UpdateBalance(deposit.Amount);
        await _accountService.Update(account.Id, account);
        log.Info($"Deposit {deposit.Id} has been created.");
        return await _transactionService.Create(deposit);
      } catch (System.Exception e) {
        log.Error(e);
        return BadRequest(new {
          error = new {
            message = e.Message
          }
        });
      }
    }

    // DELETE: api/Deposits
    [HttpDelete]
    public ActionResult DeleteDeposit() {
      log.Warn("Deny DELETE action from /api/Deposits");
      return BadRequest(new {
        error = new { message = "Delete operation not permitted on Transactions " }
      });
    }
  }
}

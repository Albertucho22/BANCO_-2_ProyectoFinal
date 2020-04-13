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
  public class LocalTransfersController : ControllerBase {
    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    private readonly TransactionService _transactionService;

    public LocalTransfersController(TransactionService transactionService) {
      _transactionService = transactionService;
    }

    // GET: api/LocalTransfers
    [HttpGet]
    public async Task<ActionResult<List<LocalTransfer>>> GetLocalTransfers() {
      try {
        List<LocalTransfer> localTransfers = await _transactionService.GetLocalTransfers();
        log.Info($"Get {localTransfers.Count} local transfers");
        return Ok(localTransfers);
      } catch (Exception e) {
        log.Error(e);
        return BadRequest(new {
          error = new {
            message = e.Message
          }
        });
      }
    }

    // GET: api/LocalTransfers/5
    [HttpGet("{id}")]
    public async Task<ActionResult<LocalTransfer>> GetLocalTransfer(int id) {
      try {
        var localTransfers = await _transactionService.GetLocalTransfers(id);
        if (localTransfers == null) {
          log.Warn($"Local Transfer {id} not found");
          return NotFound();
        }
        log.Info($"Get local transfer {id}");
        return localTransfers;
      } catch (System.Exception e) {
        log.Error(e);
        return BadRequest(new {
          error = new {
            message = e.Message
          }
        });
      }
    }

    /// PUT: api/LocalTransfers/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for
    // more details see https://aka.ms/RazorPagesCRUD.
    [HttpPut("{id}")]
    public IActionResult PutLocalTransfer() {
      log.Warn("Deny PUT action from /api/Withdrawals");
      return BadRequest(new { error = new { message = "Update operation not permitted on Transactions " } });
    }

    // POST: api/LocalTransfers
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for
    // more details see https://aka.ms/RazorPagesCRUD.
    [HttpPost]
    public async Task<ActionResult<LocalTransfer>> PostLocalTransfer(LocalTransfer localTransfer, [FromServices] AccountService _accountService) {
      try {
        var account = await _accountService.Get(localTransfer.AccountId);
        var receiverAccount = await _accountService.Get(localTransfer.ReceiverAccountId);
        if (account == null) {
          log.Warn($"Account {localTransfer.AccountId} not found.");
          return BadRequest("Account not found."); // use proper error obj
        }
        if (receiverAccount == null) {
          log.Warn($"Receiver Account {localTransfer.AccountId} not found.");
          return BadRequest("Receiver Account not found."); // use proper error obj
        }

        account.UpdateBalance(localTransfer.Amount * -1);
        await _accountService.Update(account.Id, account);
        var accountNewBalance = account.Balance;

        receiverAccount.UpdateBalance(localTransfer.Amount);
        await _accountService.Update(receiverAccount.Id, receiverAccount);
        var receiverAccountNewBalance = receiverAccount.Balance;

        log.Info($"Local Transfer {localTransfer.Id} has been created.");
        return await _transactionService.Create(localTransfer);
      } catch (System.Exception e) {
        log.Error(e);
        return BadRequest(new {
          error = new {
            message = e.Message
          }
        });
      }
    }

    // DELETE: api/LocalTransfers
    [HttpDelete]
    public ActionResult DeleteLocalTransfer() {
      log.Warn("Deny DELETE action from /api/LocalTransfers");
      return BadRequest(new {
        error = new { message = "Delete operation not permitted on Transactions " }
      });
    }
  }
}

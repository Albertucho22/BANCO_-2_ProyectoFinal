using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Services;
using WebAPI.Models;

namespace WebAPI.Controllers {
  [Route("api/[controller]")]
  [ApiController]
  public class AccountsController : ControllerBase {
    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    private readonly AccountService _accountService;

    public AccountsController(AccountService accountService) {
      _accountService = accountService;
    }

    // GET: api/Accounts
    [HttpGet]
    public async Task<ActionResult<List<Account>>> GetAccounts() {
      try {
        List<Account> accounts = await _accountService.Get();
        log.Info($"Get {accounts.Count} accounts");
        return Ok(accounts);
      } catch (System.Exception e) {
        log.Error(e);
        return BadRequest(new {
          error = new {
            message = e.Message
          }
        });
      }
    }

    // GET: api/Accounts/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Account>> GetAccount(int id) {
      try {
        var account = await _accountService.Get(id);
        if (account == null) {
          log.Warn($"Account {id} not found");
          return NotFound();
        }
        log.Info($"Get account {id}");
        return account;
      } catch (System.Exception e) {
        log.Error(e);
        return BadRequest(new {
          error = new {
            message = e.Message
          }
        });
      }
    }

    // PUT: api/Accounts/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for
    // more details see https://aka.ms/RazorPagesCRUD.
    [HttpPut("{id}")]
    public async Task<ActionResult<Account>> PutAccount(int id, Account account) {
      try {
        var updatedAccount = await _accountService.Update(id, account);
        log.Info($"Account {id} has been updated.");
        return updatedAccount;
      } catch (System.Exception e) {
        log.Error(e);
        return BadRequest(new { error = new { message = e.Message } });
      }
    }

    // POST: api/Accounts
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for
    // more details see https://aka.ms/RazorPagesCRUD.
    [HttpPost]
    public async Task<ActionResult<Account>> PostAccount(Account account, [FromServices] ClientService _clientService) {
      try {
        var client = await _clientService.Get(account.ClientId);
        Account createdAccount = await _accountService.Create(account);
        log.Info($"Account {createdAccount.Id} has been created.");
        return createdAccount;
      } catch (Exception e) {
        log.Error(e);
        return BadRequest(new { error = new { message = e.Message } });
      }
    }

    // DELETE: api/Accounts/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<Account>> DeleteAccount(int id) {
      try {
        var deletedAccount = await _accountService.Remove(id);
        log.Info($"Account {id} has been deleted.");
        return deletedAccount;
      } catch (Exception e) {
        log.Error(e);
        return BadRequest(new { error = new { message = e.Message } });
      }
    }
  }
}

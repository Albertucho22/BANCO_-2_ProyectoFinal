using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Services;
using WebAPI.Models;

namespace Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AccountsController : ControllerBase
  {
    private readonly AccountService _accountService;

    public AccountsController(AccountService accountService)
    {
      _accountService = accountService;
    }

    // GET: api/Accounts
    [HttpGet]
    public async Task<ActionResult<List<Account>>> GetAccounts()
    {
      try
      {
        List<Account> accounts = await _accountService.Get();
        return Ok(accounts);
      }
      catch (System.Exception e)
      {
        return BadRequest(new
        {
          error = new
          {
            message = e.Message
          }
        });
      }
    }

    // GET: api/Accounts/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Account>> GetAccount(int id)
    {
      try
      {
        var account = await _accountService.Get(id);
        if (account == null) return NotFound();

        return account;
      }
      catch (System.Exception e)
      {
        return BadRequest(new
        {
          error = new
          {
            message = e.Message
          }
        });
      }
    }

    // PUT: api/Accounts/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for
    // more details see https://aka.ms/RazorPagesCRUD.
    [HttpPut("{id}")]
    public async Task<ActionResult<Account>> PutAccount(int id, Account account)
    {
      try
      {
        return await _accountService.Update(id, account);
      }
      catch (System.Exception e)
      {
        return BadRequest(new { error = new { message = e.Message } });
      }
    }

    // POST: api/Accounts
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for
    // more details see https://aka.ms/RazorPagesCRUD.
    [HttpPost]
    public async Task<ActionResult<Account>> PostAccount(Account account)
    {
      try
      {
        return await _accountService.Create(account);
      }
      catch (Exception e)
      {
        return BadRequest(new { error = new { message = e.Message } });
      }
    }

    // DELETE: api/Accounts/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<Account>> DeleteAccount(int id)
    {
      try
      {
        return await _accountService.Remove(id);
      }
      catch (Exception e)
      {
        return BadRequest(new { error = new { message = e.Message } });
      }
    }
  }
}

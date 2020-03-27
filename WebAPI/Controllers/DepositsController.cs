using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class DepositsController : ControllerBase
  {
    private readonly TransactionService _transactionService;

    public DepositsController(TransactionService transactionServie)
    {
      _transactionService = transactionServie;
    }

    // GET: api/Deposits
    [HttpGet]
    public async Task<ActionResult<List<Deposit>>> GetDeposit()
    {
      try
      {
        List<Deposit> deposits = await _transactionService.GetDeposits();
        return Ok(deposits);
      }
      catch (Exception e)
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

    // GET: api/Deposits/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Deposit>> GetDeposit(int id)
    {
      try
      {
        var deposit = await _transactionService.GetDeposits(id);
        if (deposit == null) return NotFound();

        return deposit;
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

    // PUT: api/Deposits/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for
    // more details see https://aka.ms/RazorPagesCRUD.
    [HttpPut("{id}")]
    public IActionResult PutDeposit() =>
        BadRequest(new { error = new { message = "Update operation not permitted on Transactions " } });

    // POST: api/Deposits
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for
    // more details see https://aka.ms/RazorPagesCRUD.
    [HttpPost]
    public async Task<ActionResult<Deposit>> PostDeposit(Deposit deposit, [FromServices] AccountService _accountService)
    {
      try
      {
        var account = await _accountService.Get(deposit.AccountId);
        if (account == null) return BadRequest("Account not found."); // use proper error obj

        account.UpdateBalance(deposit.Amount);
        await _accountService.Update(account.Id, account);
        return await _transactionService.Create(deposit);
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

    // DELETE: api/Deposits
    [HttpDelete]
    public ActionResult DeleteDeposit() =>
        BadRequest(new
        {
          error = new { message = "Delete operation not permitted on Transactions " }
        });
  }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers {
  [Route("api/[controller]")]
  [ApiController]
  public class LoansController : ControllerBase {
    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    private readonly Core2DbContext _context;

    public LoansController(Core2DbContext context) {
      _context = context;
    }

    // GET: api/Loans
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Loan>>> GetLoan() {
      try {
        List<Loan> loans = await _context.Loan.ToListAsync();
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
        var loan = await _context.Loan.FindAsync(id);

        if (loan == null) {
          log.Warn($"Loan ${id} not found");
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
    public async Task<ActionResult<Loan>> PostLoan(Loan loan) {
      _context.Loan.Add(loan);
      await _context.SaveChangesAsync();
      log.Info($"Loan {loan.Id} has been created.");
      return CreatedAtAction("GetLoan", new { id = loan.Id }, loan);
    }

    // DELETE: api/Loans/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<Loan>> DeleteLoan(int id) {
      var loan = await _context.Loan.FindAsync(id);
      if (loan == null) {
        log.Warn($"Loan ${id} not found");
        return NotFound();
      }

      _context.Loan.Remove(loan);
      await _context.SaveChangesAsync();
      log.Info($"Loan {id} has been deleted.");
      return loan;
    }
  }
}

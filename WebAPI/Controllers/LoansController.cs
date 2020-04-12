using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoansController : ControllerBase
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly Core2DbContext _context;

        public LoansController(Core2DbContext context)
        {
            _context = context;
        }

        // GET: api/Loans
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Loan>>> GetLoan()
        {
            log.Info("All Loans geted.");
            return await _context.Loan.ToListAsync();
        }

        // GET: api/Loans/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Loan>> GetLoan(int id)
        {
            var loan = await _context.Loan.FindAsync(id);

            if (loan == null)
            {
                log.Error("the especified Loan doesn't found or doesn't exist.");
                return NotFound();
            }
            log.Info("Getted Loan specific ID");
            return loan;
        }

        // PUT: api/Loans/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLoan(int id, Loan loan)
        {
            if (id != loan.Id)
            {
                log.Error("the especified Loan doesn't found or doesn't exist.");
                return BadRequest();
            }

            _context.Entry(loan).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoanExists(id))
                {
                    log.Error("the especified Loan doesn't found or doesn't exist.");
                    return NotFound();
                }
                else
                {
                    log.Info("The Loan with the given ID has been updated.");
                    throw;
                }
            }
          //Que log va aquí???
            return NoContent();
        }

        // POST: api/Loans
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Loan>> PostLoan(Loan loan)
        {
            _context.Loan.Add(loan);
            await _context.SaveChangesAsync();
            log.Info("The Loan has been created.");
            return CreatedAtAction("GetLoan", new { id = loan.Id }, loan);
        }

        // DELETE: api/Loans/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Loan>> DeleteLoan(int id)
        {
            var loan = await _context.Loan.FindAsync(id);
            if (loan == null)
            {
                log.Error("the especified Loan doesn't found or doesn't exist.");
                return NotFound();
            }

            _context.Loan.Remove(loan);
            await _context.SaveChangesAsync();
            log.Info("The Loan has been deleted.");
            return loan;
        }

        private bool LoanExists(int id)
        {
            //Que log va aquí?
            return _context.Loan.Any(e => e.Id == id);
        }
    }
}

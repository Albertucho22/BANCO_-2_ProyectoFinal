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
    public class DepositsController : ControllerBase
    {
        private readonly Core2DbContext _context;

        public DepositsController(Core2DbContext context)
        {
            _context = context;
        }

        // GET: api/Deposits
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Deposit>>> GetDeposits()
        {
            return await _context.Deposits.ToListAsync();
        }

        // GET: api/Deposits/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Deposit>> GetDeposit(int id)
        {
            var deposit = await _context.Deposits.FindAsync(id);

            if (deposit == null)
            {
                return NotFound();
            }

            return deposit;
        }

        // PUT: api/Deposits/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDeposit(int id, Deposit deposit)
        {
            if (id != deposit.Id)
            {
                return BadRequest();
            }

            _context.Entry(deposit).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepositExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Deposits
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Deposit>> PostDeposit(Deposit deposit)
        {
            _context.Deposits.Add(deposit);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDeposit", new { id = deposit.Id }, deposit);
        }

        // DELETE: api/Deposits/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Deposit>> DeleteDeposit(int id)
        {
            var deposit = await _context.Deposits.FindAsync(id);
            if (deposit == null)
            {
                return NotFound();
            }

            _context.Deposits.Remove(deposit);
            await _context.SaveChangesAsync();

            return deposit;
        }

        private bool DepositExists(int id)
        {
            return _context.Deposits.Any(e => e.Id == id);
        }
    }
}

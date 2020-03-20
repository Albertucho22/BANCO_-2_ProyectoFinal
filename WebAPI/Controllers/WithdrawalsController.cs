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
    public class WithdrawalsController : ControllerBase
    {
        private readonly Core2DbContext _context;

        public WithdrawalsController(Core2DbContext context)
        {
            _context = context;
        }

        // GET: api/Withdrawals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Withdrawal>>> GetWithdrawal()
        {
            return await _context.Withdrawals.ToListAsync();
        }

        // GET: api/Withdrawals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Withdrawal>> GetWithdrawal(int id)
        {
            var withdrawal = await _context.Withdrawals.FindAsync(id);

            if (withdrawal == null)
            {
                return NotFound();
            }

            return withdrawal;
        }

        // PUT: api/Withdrawals/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWithdrawal(int id, Withdrawal withdrawal)
        {
            if (id != withdrawal.Id)
            {
                return BadRequest();
            }

            _context.Entry(withdrawal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WithdrawalExists(id))
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

        // POST: api/Withdrawals
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Withdrawal>> PostWithdrawal(Withdrawal withdrawal)
        {
            _context.Withdrawals.Add(withdrawal);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWithdrawal", new { id = withdrawal.Id }, withdrawal);
        }

        // DELETE: api/Withdrawals/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Withdrawal>> DeleteWithdrawal(int id)
        {
            var withdrawal = await _context.Withdrawals.FindAsync(id);
            if (withdrawal == null)
            {
                return NotFound();
            }

            _context.Withdrawals.Remove(withdrawal);
            await _context.SaveChangesAsync();

            return withdrawal;
        }

        private bool WithdrawalExists(int id)
        {
            return _context.Withdrawals.Any(e => e.Id == id);
        }
    }
}

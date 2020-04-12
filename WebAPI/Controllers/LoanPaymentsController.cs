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

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanPaymentsController : ControllerBase
    {
        private readonly TransactionService _paymentservice;

        public LoanPaymentsController(TransactionService context)
        {
            _paymentservice = context;
        }

        // GET: api/LoanPayments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LoanPayment>>> GetLoanPayments()
        {
            try
            {
                List<LoanPayment> loanPayments = await _paymentservice.GetLoanPayments();
                return Ok(loanPayments);
            }
            catch (Exception e)
            {
                return BadRequest(new {error = new {message = e.Message}});
                throw;
            }

        }
        #region 
        /*
        // GET: api/LoanPayments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LoanPayment>> GetLoanPayment(int id)

        {
            var loanPayment = await _paymentservice.GetLoanPayments(id);

            if (loanPayment == null)
            {
                return NotFound();
            }

            return loanPayment;
        }

        // PUT: api/LoanPayments/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLoanPayment(int id, LoanPayment loanPayment)
        {
            if (id != loanPayment.Id)
            {
                return BadRequest();
            }

            _paymentservice.Entry(loanPayment).State = EntityState.Modified;

            try
            {
                await _paymentservice.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoanPaymentExists(id))
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

        // POST: api/LoanPayments
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<LoanPayment>> PostLoanPayment(LoanPayment loanPayment)
        {
            _paymentservice.LoanPayments.Add(loanPayment);
            await _paymentservice.SaveChangesAsync();

            return CreatedAtAction("GetLoanPayment", new { id = loanPayment.Id }, loanPayment);
        }

        // DELETE: api/LoanPayments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<LoanPayment>> DeleteLoanPayment(int id)
        {
            var loanPayment = await _paymentservice.LoanPayments.FindAsync(id);
            if (loanPayment == null)
            {
                return NotFound();
            }

            _paymentservice.LoanPayments.Remove(loanPayment);
            await _paymentservice.SaveChangesAsync();

            return loanPayment;
        }

        private bool LoanPaymentExists(int id)
        {
            return _paymentservice.LoanPayments.Any(e => e.Id == id);
        }*/
        #endregion //cosas que arreglar
    }
}

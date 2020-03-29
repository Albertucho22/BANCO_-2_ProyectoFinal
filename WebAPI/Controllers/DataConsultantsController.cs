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
    public class DataConsultantsController : ControllerBase
    {
        private readonly Core2DbContext _context;

        public DataConsultantsController(Core2DbContext context)
        {
            _context = context;
        }

        // GET: api/DataConsultants
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DataConsultant>>> GetDataConsultants()
        {
            return await _context.DataConsultants.ToListAsync();
        }

        // GET: api/DataConsultants/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DataConsultant>> GetDataConsultant(int id)
        {
            var dataConsultant = await _context.DataConsultants.FindAsync(id);

            if (dataConsultant == null)
            {
                return NotFound();
            }

            return dataConsultant;
        }

        // PUT: api/DataConsultants/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDataConsultant(int id, DataConsultant dataConsultant)
        {
            if (id != dataConsultant.Id)
            {
                return BadRequest();
            }

            _context.Entry(dataConsultant).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DataConsultantExists(id))
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

        // POST: api/DataConsultants
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<DataConsultant>> PostDataConsultant(DataConsultant dataConsultant)
        {
            _context.DataConsultants.Add(dataConsultant);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDataConsultant", new { id = dataConsultant.Id }, dataConsultant);
        }

        // DELETE: api/DataConsultants/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DataConsultant>> DeleteDataConsultant(int id)
        {
            var dataConsultant = await _context.DataConsultants.FindAsync(id);
            if (dataConsultant == null)
            {
                return NotFound();
            }

            _context.DataConsultants.Remove(dataConsultant);
            await _context.SaveChangesAsync();

            return dataConsultant;
        }

        private bool DataConsultantExists(int id)
        {
            return _context.DataConsultants.Any(e => e.Id == id);
        }
    }
}

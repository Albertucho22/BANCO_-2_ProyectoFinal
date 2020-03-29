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
    public class DataMaintainersController : ControllerBase
    {
        private readonly Core2DbContext _context;

        public DataMaintainersController(Core2DbContext context)
        {
            _context = context;
        }

        // GET: api/DataMaintainers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DataMaintainer>>> GetDataMaintainers()
        {
            return await _context.DataMaintainers.ToListAsync();
        }

        // GET: api/DataMaintainers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DataMaintainer>> GetDataMaintainer(int id)
        {
            var dataMaintainer = await _context.DataMaintainers.FindAsync(id);

            if (dataMaintainer == null)
            {
                return NotFound();
            }

            return dataMaintainer;
        }

        // PUT: api/DataMaintainers/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDataMaintainer(int id, DataMaintainer dataMaintainer)
        {
            if (id != dataMaintainer.Id)
            {
                return BadRequest();
            }

            _context.Entry(dataMaintainer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DataMaintainerExists(id))
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

        // POST: api/DataMaintainers
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<DataMaintainer>> PostDataMaintainer(DataMaintainer dataMaintainer)
        {
            _context.DataMaintainers.Add(dataMaintainer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDataMaintainer", new { id = dataMaintainer.Id }, dataMaintainer);
        }

        // DELETE: api/DataMaintainers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DataMaintainer>> DeleteDataMaintainer(int id)
        {
            var dataMaintainer = await _context.DataMaintainers.FindAsync(id);
            if (dataMaintainer == null)
            {
                return NotFound();
            }

            _context.DataMaintainers.Remove(dataMaintainer);
            await _context.SaveChangesAsync();

            return dataMaintainer;
        }

        private bool DataMaintainerExists(int id)
        {
            return _context.DataMaintainers.Any(e => e.Id == id);
        }
    }
}

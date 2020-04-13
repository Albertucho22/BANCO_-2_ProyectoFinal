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
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly Core2DbContext _context;

        public DataMaintainersController(Core2DbContext context)
        {
            _context = context;
        }

        // GET: api/DataMaintainers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DataMaintainer>>> GetDataMaintainers()
        {
            log.Info("All DataMaintainers geted.");
            return await _context.DataMaintainers.ToListAsync();
        }

        // GET: api/DataMaintainers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DataMaintainer>> GetDataMaintainer(int id)
        {
            var dataMaintainer = await _context.DataMaintainers.FindAsync(id);

            if (dataMaintainer == null)
            {
                log.Error("The especified DataMaintainer doesn't found or doesn't exist. ");
                return NotFound();
            }
            log.Info("Getted DataMaintainer specific ID");
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
                log.Error("Ids aren't the same");
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
                    log.Error("the especified admin doesn't found or doesn't exist. ");
                    return NotFound();
                }
                else
                {
                    log.Info("The DataMaintainer with the given ID has been updated.");
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
            log.Info("The DataMaintainer has been created.");
            return CreatedAtAction("GetDataMaintainer", new { id = dataMaintainer.Id }, dataMaintainer);
        }

        // DELETE: api/DataMaintainers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DataMaintainer>> DeleteDataMaintainer(int id)
        {
            var dataMaintainer = await _context.DataMaintainers.FindAsync(id);
            if (dataMaintainer == null)
            {
                log.Error("This DataMaintainer doesn't exist");
                return NotFound();
            }

            _context.DataMaintainers.Remove(dataMaintainer);
            await _context.SaveChangesAsync();
            log.Info("The DataMaintainer has been deleted.");
            return dataMaintainer;
        }

        private bool DataMaintainerExists(int id)
        {
            return _context.DataMaintainers.Any(e => e.Id == id);
        }
    }
}

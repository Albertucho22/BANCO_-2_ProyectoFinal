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
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly Core2DbContext _context;

        public DataConsultantsController(Core2DbContext context)
        {
            _context = context;
        }

        // GET: api/DataConsultants
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DataConsultant>>> GetDataConsultants()
        {
            log.Info("All DataConsultants geted.");
            return await _context.DataConsultants.ToListAsync();
        }

        // GET: api/DataConsultants/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DataConsultant>> GetDataConsultant(int id)
        {
            var dataConsultant = await _context.DataConsultants.FindAsync(id);

            if (dataConsultant == null)
            {
                log.Error("The especified DataConsultants doesn't found or doesn't exist. ");
                return NotFound();
            }
            log.Info("Getted DataConsultant specific ID");
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
                log.Error("Ids aren't the same");
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
                    log.Error("the especified admin doesn't found or doesn't exist. ");
                    return NotFound();
                }
                else
                {
                    log.Info("The DataConsultant with the given ID has been updated.");
                    throw;
                }
            }
            //Funcion de este return?
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
            log.Info("The DataConsultant has been created.");
            return CreatedAtAction("GetDataConsultant", new { id = dataConsultant.Id }, dataConsultant);
        }

        // DELETE: api/DataConsultants/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DataConsultant>> DeleteDataConsultant(int id)
        {
            var dataConsultant = await _context.DataConsultants.FindAsync(id);
            if (dataConsultant == null)
            {
                log.Error("This DataConsultant doesn't exist");
                return NotFound();
            }

            _context.DataConsultants.Remove(dataConsultant);
            await _context.SaveChangesAsync();
            log.Info("The DataConsultant has been deleted.");
            return dataConsultant;
        }

        private bool DataConsultantExists(int id)
        {
            //Función de este return?
            return _context.DataConsultants.Any(e => e.Id == id);
        }
    }
}

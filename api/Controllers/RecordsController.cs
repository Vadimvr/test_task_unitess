using db;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using models;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RecordsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Record>>> GetRecord()
        {
          if (_context.Records == null)
          {
              return NotFound();
          }
            return await _context.Records.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Record>> GetRecord(int id)
        {
          if (_context.Records == null)
          {
              return NotFound();
          }
            var record = await _context.Records.FindAsync(id);

            if (record == null)
            {
                return NotFound();
            }

            return record;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecord(int id, Record record)
        {
            if (id != record.Id)
            {
                return BadRequest();
            }

            _context.Entry(record).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!recordExists(id))
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

        [HttpPost]
        public async Task<ActionResult<Record>> PostRecord(Record record)
        {
          if (_context.Records == null)
          {
              return Problem("Entity set 'apiContext.Record'  is null.");
          }
            _context.Records.Add(record);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRecord", new { id = record.Id }, record);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deleterecord(int id)
        {
            if (_context.Records == null)
            {
                return NotFound();
            }
            var record = await _context.Records.FindAsync(id);
            if (record == null)
            {
                return NotFound();
            }

            _context.Records.Remove(record);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool recordExists(int id)
        {
            return (_context.Records?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

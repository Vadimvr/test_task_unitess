using db;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Differencing;
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
        #region Get all
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecordDTO>>> GetRecord(int? startId, int? endId)
        {

            if (_context.Records == null)
            {
                return NotFound();
            }

            if (startId == null && endId == null)
            {
                return await _context.Records
                    .Select(r => RecordDTO.ConvertToRecordDTO(r))
                    .ToListAsync();
            }

            if (startId < 0 || endId < 0 || startId == null || endId == null)
            {
                return BadRequest();
            }

            if (startId > endId)
            {
                var temp = startId;
                startId = endId;
                endId = temp;
            }
            return await _context.Records
                .Where(x => (x.Id >= startId && x.Id <= endId))
                .Select(r => RecordDTO.ConvertToRecordDTO(r))
                .ToListAsync();
        }
        #endregion

        #region Get by id
        [HttpGet("{id}")]
        public async Task<ActionResult<RecordDTO>> GetRecord(int id)
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
            Console.WriteLine(record.HiddenText);
            return RecordDTO.ConvertToRecordDTO(record);
        }
        #endregion

        #region Put

        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecord(int id, Record recordNew)
        {
            if (id != recordNew.Id) { return BadRequest(); }
            var record = _context.Records.FirstOrDefault(x => x.Id == id);
            if (record == null) { return NotFound(); }

            record.Text = recordNew.Text;
            record.Date = recordNew.Date;
            record.HiddenText = recordNew.HiddenText;

            _context.Entry(record).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecordExists(id))
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
        #endregion

        #region Post    
        [HttpPost]
        public async Task<ActionResult<Record>> PostRecord(Record record)
        {
            if (_context.Records == null)
            {
                return Problem("Entity set 'apiContext.Record'  is null.");
            }
            _context.Records.Add(record);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRecord", new { id = record.Id }, RecordDTO.ConvertToRecordDTO(record));
        }
        #endregion

        #region Delete

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
        #endregion

        #region Methods
        private bool RecordExists(int id)
        {
            return (_context.Records?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        #endregion
    }
}

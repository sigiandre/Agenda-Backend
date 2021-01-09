using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgendaAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AgendaAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class DCandidateController : ControllerBase
    {
        private readonly DonationDBContext _context;

        public DCandidateController(DonationDBContext context)
        {
            _context = context;
        }

        // GET: api/Candidate
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DCandidate>>> GetDCandidates()
        {
            return await _context.DCandidates.ToListAsync();
        }

        // GET api/Candidate/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DCandidate>> GetDCandidate(int id)
        {
            var dCandidate = await _context.DCandidates.FindAsync(id);

            if (dCandidate == null)
            {
                return NotFound();
            }

            return dCandidate;
        }

        // PUT api/Candidate/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDCandidate(int id, DCandidate dCandidate)
        {
            dCandidate.id = id;

            _context.Entry(dCandidate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DCandidateExists(id))
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

        // POST api/Candidate
        [HttpPost]
        public async Task<ActionResult<DCandidate>> PostDCandidate(DCandidate dCandidate)
        {
            _context.DCandidates.Add(dCandidate);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDCandidate", new { id = dCandidate.id}, dCandidate);
        }

        // DELETE api/Candidate/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DCandidate>> DeleteDCandidate(int id)
        {
            var dCandidate = await _context.DCandidates.FindAsync(id);

            if (dCandidate == null)
            {
                return NotFound();
            }

            _context.DCandidates.Remove(dCandidate);
            await _context.SaveChangesAsync();

            return dCandidate;
        }

        private bool DCandidateExists(int id)
        {
            return _context.DCandidates.Any(e => e.id == id);
        }
    }
}

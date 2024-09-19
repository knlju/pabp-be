using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication8.Models;

namespace WebApplication8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PredmetsController : ControllerBase
    {
        private readonly StudentskaContext _context;

        public PredmetsController(StudentskaContext context)
        {
            _context = context;
        }

        // GET: api/Predmets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Predmet>>> GetPredmets()
        {
            return await _context.Predmets.ToListAsync();
        }

        // GET: api/Predmets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Predmet>> GetPredmet(short id)
        {
            var predmet = await _context.Predmets.FindAsync(id);

            if (predmet == null)
            {
                return NotFound();
            }

            return predmet;
        }

        // PUT: api/Predmets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPredmet(short id, Predmet predmet)
        {
            if (id != predmet.IdPredmeta)
            {
                return BadRequest();
            }

            _context.Entry(predmet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PredmetExists(id))
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

        // POST: api/Predmets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Predmet>> PostPredmet(Predmet predmet)
        {
            _context.Predmets.Add(predmet);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PredmetExists(predmet.IdPredmeta))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPredmet", new { id = predmet.IdPredmeta }, predmet);
        }

        // DELETE: api/Predmets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePredmet(short id)
        {
            var predmet = await _context.Predmets.FindAsync(id);
            if (predmet == null)
            {
                return NotFound();
            }

            _context.Predmets.Remove(predmet);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PredmetExists(short id)
        {
            return _context.Predmets.Any(e => e.IdPredmeta == id);
        }
    }
}

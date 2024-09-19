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
    public class ZapisniksController : ControllerBase
    {
        private readonly StudentskaContext _context;

        public ZapisniksController(StudentskaContext context)
        {
            _context = context;
        }

        // GET: api/Zapisniks/5/3
        [HttpGet("Student/{IdStudenta}/Ispit/{IdIspita}")]
        public async Task<ActionResult<Zapisnik>> GetZapisnik(int IdStudenta, int IdIspita)
        {
            var zapisnik = await _context.Zapisniks.FindAsync(IdStudenta, IdIspita);

            if (zapisnik == null)
            {
                return NotFound();
            }

            return Ok(zapisnik);
        }

        // POST: api/Zapisniks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Zapisnik>> PostZapisnik(Zapisnik zapisnik)
        {
            if (zapisnik.Ocena <= 5) return BadRequest("Ocena must be greater than 5");
                _context.Zapisniks.Add(zapisnik);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ZapisnikExists(zapisnik.IdStudenta, zapisnik.IdIspita))
                {
                    return Conflict("Ispit je vec polozen");
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetZapisnik", new { IdStudenta = zapisnik.IdStudenta, IdIspita = zapisnik.IdIspita }, zapisnik);
        }

        private bool ZapisnikExists(int IdStudenta, int IdIspita)
        {
            return _context.Zapisniks.Any(e => e.IdStudenta == IdStudenta && e.IdIspita == IdIspita);
        }
    }
}

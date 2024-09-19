using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;
using WebApplication8.Models;

namespace WebApplication8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentPredmetsController : ControllerBase
    {
        private readonly StudentskaContext _context;

        public StudentPredmetsController(StudentskaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<StudentPredmet>>> GetAll()
        {
            return await _context.StudentPredmets.ToListAsync();
        }

        [HttpGet("{IdStudenta}/{IdPredmeta}/{SkolskaGodina}")]
        public async Task<ActionResult<StudentPredmet>> GetStudentPredmet(int IdStudenta, short IdPredmeta, string SkolskaGodina)
        {
            var sp = await _context.StudentPredmets.FindAsync(IdStudenta, IdPredmeta, SkolskaGodina);
            if (sp == null)
            {
                return NotFound();
            }
            return Ok(sp);
        }

        // GET: api/StudentPredmets/Student/5/Predmets
        [HttpGet("{studentId}/Predmets")]
        public async Task<ActionResult> GetStudentsPredmets(int studentId)
        {
            var predmetiStudenta = await _context.StudentPredmets
                .Where(studentPredmet => studentPredmet.IdStudenta == studentId)
                .Include(x => x.IdPredmetaNavigation)
                .Select(x => new
                {
                    Predmet = x.IdPredmetaNavigation,
                    SkolskaGodina = x.SkolskaGodina
                })
                .ToListAsync();

            return Ok(predmetiStudenta);
        }

        // GET api/StudentPredmet/Student/5/Prijava
        [HttpGet("Student/{idStudenta}/Prijava")]
        public async Task<ActionResult<ICollection<Predmet>>> GetPrijava(int idStudenta)
        {
            var nepolozeniPredmeti = await _context.StudentPredmets.AsNoTracking()
                .Where(sp =>
                    sp.IdStudenta == idStudenta && 
                    !sp.IdStudentaNavigation.Zapisniks.Any(
                        z => z.IdIspitaNavigation.IdPredmeta == sp.IdPredmeta
                    )
                )
                .Select(x => new
                {
                    Predmet = x.IdPredmetaNavigation,
                    SkolskaGodina = x.SkolskaGodina
                })
                .ToListAsync();
            return Ok(nepolozeniPredmeti);
        }

        // POST: api/StudentPredmets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StudentPredmet>> PostStudentPredmet(StudentPredmetDTO studentPredmetDTO)
        {
            var studentPredmet = new StudentPredmet
            {
                IdStudenta = studentPredmetDTO.IdStudenta,
                IdPredmeta = studentPredmetDTO.IdPredmeta,
                SkolskaGodina = studentPredmetDTO.SkolskaGodina
            };
            _context.StudentPredmets.Add(studentPredmet);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (StudentPredmetExists(studentPredmetDTO.IdStudenta, studentPredmetDTO.IdPredmeta, studentPredmetDTO.SkolskaGodina))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction(
                "GetStudentPredmet", 
                new { studentPredmet.IdStudenta, studentPredmet.IdPredmeta, studentPredmet.SkolskaGodina },
                studentPredmet);
        }

        // DELETE: api/StudentPredmets/5
        [HttpDelete("Student/{idStudenta}/Predmet/{idPredmeta}/SkolskaGodina/{skolskaGodina}")]
        public async Task<IActionResult> DeleteStudentPredmet(int idStudenta, short idPredmeta, string skolskaGodina)
        {
            var skDecoded = HttpUtility.UrlDecode(skolskaGodina);
            var studentPredmet = await _context.StudentPredmets.FindAsync(idStudenta, idPredmeta, skDecoded);
            if (studentPredmet == null)
            {
                return NotFound();
            }

            var predmetPolozen = _context.StudentPredmets.Any(sp => sp.IdPredmeta == idPredmeta && sp.IdStudenta == idStudenta && sp.SkolskaGodina == skDecoded
            && sp.IdStudentaNavigation.Zapisniks.Any(z=> z.IdIspitaNavigation.IdPredmeta == idPredmeta));
                //_context.Zapisniks.Any(z => z.IdStudenta == idStudenta && z.IdIspitaNavigation.IdPredmeta == idPredmeta);
            if (predmetPolozen)
            {
                return Conflict("Nemoguce obrisati vec polozen predmet.");
            }

            _context.StudentPredmets.Remove(studentPredmet);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StudentPredmetExists(int IdStudenta, int IdPredmeta, string SkolskaGodina)
        {
            return _context.StudentPredmets.Any(e => e.IdStudenta == IdStudenta && e.IdPredmeta == IdPredmeta && e.SkolskaGodina == SkolskaGodina);
        }
    }

    public class StudentPredmetDTO
    {
        public int IdStudenta { get; set; }
        public short IdPredmeta { get; set; }
        public string SkolskaGodina { get; set; }
    }
}

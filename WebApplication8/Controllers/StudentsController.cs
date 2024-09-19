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
    public class StudentsController : ControllerBase
    {
        private readonly StudentskaContext _context;

        public StudentsController(StudentskaContext context)
        {
            _context = context;
        }

        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult> GetStudents()
        {
            var students = await _context.Students.Select(s => new
            {
                Id = s.IdStudenta,
                Ime = s.Ime,
                Prezime = s.Prezime,
                BrojIndeksa = $"{s.Smer}-{s.Broj}/{s.GodinaUpisa}",
            }).ToListAsync();

            return Ok(students);
        }

        // PATCH: api/Students/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchStudent(int id, [FromBody] StudentPatchBody studentParams)
        {

            var studentFound = await _context.Students.FindAsync(id);

            if (studentFound == null)
            {
                return NotFound();
            }

            studentFound.Ime = studentParams.Ime;
            studentFound.Prezime = studentParams.Prezime;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }

        [HttpGet("pretraga")]
        public async Task<ActionResult<ICollection<Student>>> SearchStudent(
                [FromQuery] string? Ime,
                [FromQuery] string? Prezime,
                [FromQuery] string? Smer,
                [FromQuery] short? Broj,
                [FromQuery] string? GodinaUpisa
            )
        {
            var query = _context.Students.AsQueryable();

            if (!string.IsNullOrEmpty(Ime))
            {
                query = query.Where(s => s.Ime.Contains(Ime));
            }

            if (!string.IsNullOrEmpty(Prezime))
            {
                query = query.Where(s => s.Prezime.Contains(Prezime));
            }

            if (!string.IsNullOrEmpty(Smer))
            {
                query = query.Where(s => s.Smer.Contains(Smer));
            }

            if (!string.IsNullOrEmpty(GodinaUpisa))
            {
                query = query.Where(s => s.GodinaUpisa.Contains(GodinaUpisa));
            }

            if (Broj != null)
            {
                query = query.Where(s => s.Broj == Broj);
            }

            var students = await query.Select(s => new
            {
                Id = s.IdStudenta,
                Ime = s.Ime,
                Prezime = s.Prezime,
                BrojIndeksa = $"{s.Smer}-{s.Broj}/{s.GodinaUpisa}",
                Prosek = s.Zapisniks.Any() ? s.Zapisniks.Average(z => z.Ocena) : 0
            }).ToListAsync();

            return Ok(students);
        }

        [HttpGet("/polozeni/{StudentId}")]
        public async Task<ActionResult> GetPolozeniPredmeti(int StudentId)
        {
            var polozeni = await _context.Zapisniks
                .Where(z => z.IdStudenta == StudentId)
                .Select(z => z.IdIspitaNavigation.IdPredmetaNavigation)
                .ToListAsync();
            var prosek = _context.Zapisniks
                .Where(z => z.IdStudenta == StudentId)
                .Select(z => z.Ocena)
                .DefaultIfEmpty(0)
                .Average();
            return Ok(new { polozeni, prosek });
        }
    }

    public class StudentPatchBody
    {
        public string Ime { get; set; } = null!;
        public string Prezime { get; set; } = null!;
    }
}

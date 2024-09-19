using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public class Prijava_brojIndeksaController : ControllerBase
    {
        private readonly StudentskaContext _context;

        public Prijava_brojIndeksaController(StudentskaContext context)
        {
            _context = context;
        }

        [HttpGet("Ispit/{IspitId}")]
        public async Task<ActionResult<ICollection<Prijava_brojIndeksa>>> GetIspitPrijavas(int IspitId)
        {
            var prijave = await _context.Prijava_BrojIndeksas
                .AsNoTracking()
                .Where(pbi => pbi.IdIspita == IspitId)
                .ToListAsync();

            return Ok(prijave);
        }

        [HttpPost]
        public async Task<ActionResult<Prijava_brojIndeksa>> PostPrijava([FromBody] Prijava_brojIndeksa prijava)
        {
            var exists = await _context.Prijava_BrojIndeksas.FindAsync(prijava.IdStudenta, prijava.IdIspita);
            if (exists != null)
            {
                return Conflict("Prijava vec postoji");
            }

            var existsStudent = await _context.Students.FindAsync(prijava.IdStudenta);
            var existsIspit = await _context.Ispits.FindAsync(prijava.IdIspita);

            if(existsIspit==null || existsStudent==null)
            {
                return BadRequest("Neispravni student ili ispit");
            }

            _context.Prijava_BrojIndeksas.Add(prijava);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPrijava", new { StudentId = prijava.IdStudenta, IspitId = prijava.IdIspita }, prijava);
        }

        [HttpGet("Student/{StudentId}/Ispit/{IspitId}")]
        public async Task<ActionResult<Prijava_brojIndeksa>> GetPrijava(int StudentId, int IspitId)
        {
            var prijava = await _context.Prijava_BrojIndeksas.FindAsync(StudentId, IspitId);
            if (prijava == null) return NotFound();
            return Ok(prijava);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppDistribuidas.Models;

namespace AppDistribuidas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalificacionesController : ControllerBase
    {
        private readonly ApplicacionesDistribuidasContext _context;

        public CalificacionesController(ApplicacionesDistribuidasContext context)
        {
            _context = context;
        }

        // GET: api/Calificaciones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Calificacione>>> GetCalificaciones()
        {
          if (_context.Calificaciones == null)
          {
              return NotFound();
          }
            return await _context.Calificaciones.ToListAsync();
        }

        // GET: api/Calificaciones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Calificacione>> GetCalificacione(int id)
        {
          if (_context.Calificaciones == null)
          {
              return NotFound();
          }
            var calificacione = await _context.Calificaciones.FindAsync(id);

            if (calificacione == null)
            {
                return NotFound();
            }

            return calificacione;
        }

        // PUT: api/Calificaciones/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCalificacione(int id, Calificacione calificacione)
        {
            if (id != calificacione.IdCalificacion)
            {
                return BadRequest();
            }

            _context.Entry(calificacione).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CalificacioneExists(id))
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

        // POST: api/Calificaciones
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Calificacione>> PostCalificacione(Calificacione calificacione)
        {
          if (_context.Calificaciones == null)
          {
              return Problem("Entity set 'ApplicacionesDistribuidasContext.Calificaciones'  is null.");
          }
            _context.Calificaciones.Add(calificacione);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCalificacione", new { id = calificacione.IdCalificacion }, calificacione);
        }

        // DELETE: api/Calificaciones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCalificacione(int id)
        {
            if (_context.Calificaciones == null)
            {
                return NotFound();
            }
            var calificacione = await _context.Calificaciones.FindAsync(id);
            if (calificacione == null)
            {
                return NotFound();
            }

            _context.Calificaciones.Remove(calificacione);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CalificacioneExists(int id)
        {
            return (_context.Calificaciones?.Any(e => e.IdCalificacion == id)).GetValueOrDefault();
        }
    }
}

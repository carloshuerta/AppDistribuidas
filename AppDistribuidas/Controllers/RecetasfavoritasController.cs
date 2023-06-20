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
    public class RecetasfavoritasController : ControllerBase
    {
        private readonly ApplicacionesDistribuidasContext _context;

        public RecetasfavoritasController(ApplicacionesDistribuidasContext context)
        {
            _context = context;
        }

        // GET: api/Recetasfavoritas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Recetasfavorita>>> GetRecetasfavoritas()
        {
          if (_context.Recetasfavoritas == null)
          {
              return NotFound();
          }
            return await _context.Recetasfavoritas.ToListAsync();
        }

        // GET: api/Recetasfavoritas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Recetasfavorita>> GetRecetasfavorita(int id)
        {
          if (_context.Recetasfavoritas == null)
          {
              return NotFound();
          }
            var recetasfavorita = await _context.Recetasfavoritas.FindAsync(id);

            if (recetasfavorita == null)
            {
                return NotFound();
            }

            return recetasfavorita;
        }

        // PUT: api/Recetasfavoritas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecetasfavorita(int id, Recetasfavorita recetasfavorita)
        {
            if (id != recetasfavorita.IdRecetaFavorita)
            {
                return BadRequest();
            }

            _context.Entry(recetasfavorita).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecetasfavoritaExists(id))
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

        // POST: api/Recetasfavoritas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Recetasfavorita>> PostRecetasfavorita(Recetasfavorita recetasfavorita)
        {
          if (_context.Recetasfavoritas == null)
          {
              return Problem("Entity set 'ApplicacionesDistribuidasContext.Recetasfavoritas'  is null.");
          }
            _context.Recetasfavoritas.Add(recetasfavorita);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (RecetasfavoritaExists(recetasfavorita.IdRecetaFavorita))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetRecetasfavorita", new { id = recetasfavorita.IdRecetaFavorita }, recetasfavorita);
        }

        // DELETE: api/Recetasfavoritas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecetasfavorita(int id)
        {
            if (_context.Recetasfavoritas == null)
            {
                return NotFound();
            }
            var recetasfavorita = await _context.Recetasfavoritas.FindAsync(id);
            if (recetasfavorita == null)
            {
                return NotFound();
            }

            _context.Recetasfavoritas.Remove(recetasfavorita);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RecetasfavoritaExists(int id)
        {
            return (_context.Recetasfavoritas?.Any(e => e.IdRecetaFavorita == id)).GetValueOrDefault();
        }
    }
}

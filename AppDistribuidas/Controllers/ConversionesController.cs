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
    public class ConversionesController : ControllerBase
    {
        private readonly ApplicacionesDistribuidasContext _context;

        public ConversionesController(ApplicacionesDistribuidasContext context)
        {
            _context = context;
        }

        // GET: api/Conversiones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Conversione>>> GetConversiones()
        {
          if (_context.Conversiones == null)
          {
              return NotFound();
          }
            return await _context.Conversiones.ToListAsync();
        }

        // GET: api/Conversiones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Conversione>> GetConversione(int id)
        {
          if (_context.Conversiones == null)
          {
              return NotFound();
          }
            var conversione = await _context.Conversiones.FindAsync(id);

            if (conversione == null)
            {
                return NotFound();
            }

            return conversione;
        }

        // PUT: api/Conversiones/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConversione(int id, Conversione conversione)
        {
            if (id != conversione.IdConversion)
            {
                return BadRequest();
            }

            _context.Entry(conversione).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConversioneExists(id))
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

        // POST: api/Conversiones
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Conversione>> PostConversione(Conversione conversione)
        {
          if (_context.Conversiones == null)
          {
              return Problem("Entity set 'ApplicacionesDistribuidasContext.Conversiones'  is null.");
          }
            _context.Conversiones.Add(conversione);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetConversione", new { id = conversione.IdConversion }, conversione);
        }

        // DELETE: api/Conversiones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConversione(int id)
        {
            if (_context.Conversiones == null)
            {
                return NotFound();
            }
            var conversione = await _context.Conversiones.FindAsync(id);
            if (conversione == null)
            {
                return NotFound();
            }

            _context.Conversiones.Remove(conversione);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ConversioneExists(int id)
        {
            return (_context.Conversiones?.Any(e => e.IdConversion == id)).GetValueOrDefault();
        }
    }
}

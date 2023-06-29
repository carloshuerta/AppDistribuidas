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
    public class MultimediumsController : ControllerBase
    {
        private readonly ApplicacionesDistribuidasContext _context;

        public MultimediumsController(ApplicacionesDistribuidasContext context)
        {
            _context = context;
        }

        // GET: api/Multimediums
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Multimedium>>> GetMultimedia()
        {
          if (_context.Multimedia == null)
          {
              return NotFound();
          }
            return await _context.Multimedia.ToListAsync();
        }

        // GET: api/Utilizadoes
        [HttpGet("MultimediumPorPaso/{idPaso}")]
        public async Task<ActionResult<IEnumerable<Multimedium>>> MultimediumPorPaso(int idPaso)
        {
            if (_context.Multimedia == null)
            {
                return NotFound();
            }
            return await _context.Multimedia.Where(x => x.IdPaso.Equals(idPaso)).ToListAsync();
        }

        // GET: api/Multimediums/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Multimedium>> GetMultimedium(int id)
        {
          if (_context.Multimedia == null)
          {
              return NotFound();
          }
            var multimedium = await _context.Multimedia.FindAsync(id);

            if (multimedium == null)
            {
                return NotFound();
            }

            return multimedium;
        }

        // PUT: api/Multimediums/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMultimedium(int id, Multimedium multimedium)
        {
            if (id != multimedium.IdContenido)
            {
                return BadRequest();
            }

            _context.Entry(multimedium).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MultimediumExists(id))
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

        // POST: api/Multimediums
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Multimedium>> PostMultimedium(Multimedium multimedium)
        {
          if (_context.Multimedia == null)
          {
              return Problem("Entity set 'ApplicacionesDistribuidasContext.Multimedia'  is null.");
          }
            _context.Multimedia.Add(multimedium);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMultimedium", new { id = multimedium.IdContenido }, multimedium);
        }

        // DELETE: api/Multimediums/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMultimedium(int id)
        {
            if (_context.Multimedia == null)
            {
                return NotFound();
            }
            var multimedium = await _context.Multimedia.FindAsync(id);
            if (multimedium == null)
            {
                return NotFound();
            }

            _context.Multimedia.Remove(multimedium);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MultimediumExists(int id)
        {
            return (_context.Multimedia?.Any(e => e.IdContenido == id)).GetValueOrDefault();
        }
    }
}

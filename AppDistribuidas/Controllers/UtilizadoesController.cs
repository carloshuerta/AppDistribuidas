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
    public class UtilizadoesController : ControllerBase
    {
        private readonly ApplicacionesDistribuidasContext _context;

        public UtilizadoesController(ApplicacionesDistribuidasContext context)
        {
            _context = context;
        }

        // GET: api/Utilizadoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Utilizado>>> GetUtilizados()
        {
          if (_context.Utilizados == null)
          {
              return NotFound();
          }
            return await _context.Utilizados.ToListAsync();
        }

        // GET: api/Utilizadoes
        [HttpGet("utilizadoPorRecetas/{idReceta}")]
        public async Task<ActionResult<IEnumerable<Utilizado>>> UtilizadoPorRecetas(int idReceta)
        {
            if (_context.Utilizados == null)
            {
                return NotFound();
            }
            return await _context.Utilizados.Where(x => x.IdReceta.Equals(idReceta)).ToListAsync();
        }

        // GET: api/Utilizadoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Utilizado>> GetUtilizado(int id)
        {
          if (_context.Utilizados == null)
          {
              return NotFound();
          }
            var utilizado = await _context.Utilizados.FindAsync(id);

            if (utilizado == null)
            {
                return NotFound();
            }

            return utilizado;
        }

        // PUT: api/Utilizadoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUtilizado(int id, Utilizado utilizado)
        {
            if (id != utilizado.IdUtilizado)
            {
                return BadRequest();
            }

            _context.Entry(utilizado).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UtilizadoExists(id))
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

        // POST: api/Utilizadoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Utilizado>> PostUtilizado(Utilizado utilizado)
        {
          if (_context.Utilizados == null)
          {
              return Problem("Entity set 'ApplicacionesDistribuidasContext.Utilizados'  is null.");
          }
            _context.Utilizados.Add(utilizado);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUtilizado", new { id = utilizado.IdUtilizado }, utilizado);
        }

        // DELETE: api/Utilizadoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUtilizado(int id)
        {
            if (_context.Utilizados == null)
            {
                return NotFound();
            }
            var utilizado = await _context.Utilizados.FindAsync(id);
            if (utilizado == null)
            {
                return NotFound();
            }

            _context.Utilizados.Remove(utilizado);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UtilizadoExists(int id)
        {
            return (_context.Utilizados?.Any(e => e.IdUtilizado == id)).GetValueOrDefault();
        }
    }
}

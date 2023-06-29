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
    public class FotoesController : ControllerBase
    {
        private readonly ApplicacionesDistribuidasContext _context;

        public FotoesController(ApplicacionesDistribuidasContext context)
        {
            _context = context;
        }

        // GET: api/Fotoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Foto>>> GetFotos()
        {
          if (_context.Fotos == null)
          {
              return NotFound();
          }
            return await _context.Fotos.ToListAsync();
        }

        // GET: api/Utilizadoes
        [HttpGet("fotoPorRecetas/{idReceta}")]
        public async Task<ActionResult<IEnumerable<Foto>>> CalificacionePorRecetas(int idReceta)
        {
            if (_context.Fotos == null)
            {
                return NotFound();
            }
            return await _context.Fotos.Where(x => x.IdReceta.Equals(idReceta)).ToListAsync();
        }

        // GET: api/Fotoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Foto>> GetFoto(int id)
        {
          if (_context.Fotos == null)
          {
              return NotFound();
          }
            var foto = await _context.Fotos.FindAsync(id);

            if (foto == null)
            {
                return NotFound();
            }

            return foto;
        }

        // PUT: api/Fotoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFoto(int id, Foto foto)
        {
            if (id != foto.Idfoto)
            {
                return BadRequest();
            }

            _context.Entry(foto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FotoExists(id))
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

        // POST: api/Fotoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Foto>> PostFoto(Foto foto)
        {
          if (_context.Fotos == null)
          {
              return Problem("Entity set 'ApplicacionesDistribuidasContext.Fotos'  is null.");
          }
            _context.Fotos.Add(foto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFoto", new { id = foto.Idfoto }, foto);
        }

        // DELETE: api/Fotoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFoto(int id)
        {
            if (_context.Fotos == null)
            {
                return NotFound();
            }
            var foto = await _context.Fotos.FindAsync(id);
            if (foto == null)
            {
                return NotFound();
            }

            _context.Fotos.Remove(foto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FotoExists(int id)
        {
            return (_context.Fotos?.Any(e => e.Idfoto == id)).GetValueOrDefault();
        }
    }
}

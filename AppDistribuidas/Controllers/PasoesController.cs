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
    public class PasosController : ControllerBase
    {
        private readonly ApplicacionesDistribuidasContext _context;

        public PasosController(ApplicacionesDistribuidasContext context)
        {
            _context = context;
        }

        // GET: api/Pasos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Paso>>> GetPasos()
        {
          if (_context.Pasos == null)
          {
              return NotFound();
          }
            return await _context.Pasos.ToListAsync();
        }

        // GET: api/Pasos
        [HttpGet("pasosPorRecetas/{idReceta}")]
        public async Task<ActionResult<IEnumerable<Paso>>> GetPasosPorRecetas(int idReceta)
        {
            if (_context.Pasos == null)
            {
                return NotFound();
            }



            return await _context.Pasos.Where(x => x.IdReceta.Equals(idReceta)).ToListAsync();
        }

        // GET: api/Pasos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Paso>> GetPaso(int id)
        {
          if (_context.Pasos == null)
          {
              return NotFound();
          }
            var paso = await _context.Pasos.FindAsync(id);

            if (paso == null)
            {
                return NotFound();
            }

            return paso;
        }

        // PUT: api/Pasos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaso(int id, Paso paso)
        {
            if (id != paso.IdPaso)
            {
                return BadRequest();
            }

            _context.Entry(paso).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PasoExists(id))
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

        // POST: api/Pasos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Paso>> PostPaso(Paso paso)
        {
          if (_context.Pasos == null)
          {
              return Problem("Entity set 'ApplicacionesDistribuidasContext.Pasos'  is null.");
          }
            _context.Pasos.Add(paso);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPaso", new { id = paso.IdPaso }, paso);
        }

        // DELETE: api/Pasos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaso(int id)
        {
            if (_context.Pasos == null)
            {
                return NotFound();
            }
            var paso = await _context.Pasos.FindAsync(id);
            if (paso == null)
            {
                return NotFound();
            }

            _context.Pasos.Remove(paso);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PasoExists(int id)
        {
            return (_context.Pasos?.Any(e => e.IdPaso == id)).GetValueOrDefault();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppDistribuidas.Models;
using System.Linq.Expressions;
using NuGet.Protocol;

namespace AppDistribuidas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecetasController : ControllerBase
    {
        private readonly ApplicacionesDistribuidasContext _context;

        public RecetasController(ApplicacionesDistribuidasContext context)
        {
            _context = context;
        }

        // GET: api/Recetas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Receta>>> GetRecetas()
        {
          if (_context.Recetas == null)
          {
              return NotFound();
          }
            return await _context.Recetas.ToListAsync();
        }

        // GET: api/Recetas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Receta>> GetReceta(int id)
        {
          if (_context.Recetas == null)
          {
              return NotFound();
          }
            var receta = await _context.Recetas.FindAsync(id);

            if (receta == null)
            {
                return NotFound();
            }

            return receta;
        }

        // GET: api/Recetas/5
        [HttpGet("buscar/pornombre/{name}")]
        public async Task<ActionResult<Receta>> GetRecetaPorNombre(string name)
        {
            if (_context.Recetas == null)
            {
                return NotFound();
            }
            var recetas = await _context.Recetas.ToListAsync();
            foreach (var receta in recetas) 
            { 
             if (receta.Nombre == name)
                {
                    return receta;
                }
            
            }

            return NotFound();

        }

        // GET: api/portipo/tipo
        [HttpGet("buscar/portipo/{tipo}")]
        public async Task<ActionResult<IEnumerable<Receta>>> GetRecetaPorTipo(string tipo)
        {
            if (_context.Recetas == null)
            {
                return NotFound();
            }
            List<Receta> encontrados = new List<Receta>();

            var tipos = await _context.Tipos.ToListAsync();

            foreach (var tipoIndividual in tipos)
            { 
                if(tipoIndividual.Descripcion == tipo)
                {
                    var recetas = await _context.Recetas.ToListAsync();
                    foreach (var receta in recetas)
                    {
                        if (receta.IdTipo == tipoIndividual.IdTipo)
                        {
                            encontrados.Add(receta);
                        }

                    }

                    return encontrados.ToArray<Receta>();
                }
            }
            return NotFound();
        }

        // GET: api/poringrediente/ingrediente
        [HttpGet("buscar/poringrediente/{ingrediente}")]
        public async Task<ActionResult<IEnumerable<Receta>>> GetRecetaPorIngrediente(string ingrediente)
        {
            if (_context.Recetas == null)
            {
                return NotFound();
            }
            List<Receta> encontrados = new List<Receta>();

            var ingredientes = await _context.Ingredientes.ToListAsync();

            foreach (var ingredienteIndividual in ingredientes)
            {
                if (ingredienteIndividual.Nombre == ingrediente)
                {
                    var utilizados = await _context.Utilizados.ToListAsync();
                    foreach (var utilizado in utilizados)
                    {
                        var recetas = await _context.Recetas.ToListAsync();
                        foreach (var receta in recetas)
                        {
                            if (utilizado.IdReceta == receta.IdReceta && utilizado.IdIngrediente == ingredienteIndividual.IdIngrediente)
                            {
                                encontrados.Add(receta);
                            }

                        }

                        return encontrados.ToArray<Receta>();
                    }
                }
            }
            return NotFound();
        }


        // GET: api/porusuario/usuario
        [HttpGet("buscar/porusuario/{usuario}")]
        public async Task<ActionResult<IEnumerable<Receta>>> GetRecetaPorUsuario(string usuario)
        {
            if (_context.Recetas == null)
            {
                return NotFound();
            }
            List<Receta> encontrados = new List<Receta>();

            var usuarios = await _context.Usuarios.ToListAsync();

            foreach (var usuarioIndividual in usuarios)
            {
                if (usuarioIndividual.Nombre == usuario)
                {
                    var recetas = await _context.Recetas.ToListAsync();
                    foreach (var receta in recetas)
                    {
                        if (receta.IdUsuario == usuarioIndividual.IdUsuario)
                        {
                            encontrados.Add(receta);
                        }

                    }

                    return encontrados.ToArray<Receta>();
                }
            }
            return NotFound();
        }

        // GET: api/porusuario/usuario
        [HttpGet("buscar/pornickname/{usuario}")]
        public async Task<ActionResult<IEnumerable<Receta>>> GetRecetaPorNickname(string nickname)
        {
            if (_context.Recetas == null)
            {
                return NotFound();
            }
            List<Receta> encontrados = new List<Receta>();

            var usuarios = await _context.Usuarios.ToListAsync();

            foreach (var usuarioIndividual in usuarios)
            {
                if (usuarioIndividual.Nickname == nickname)
                {
                    var recetas = await _context.Recetas.ToListAsync();
                    foreach (var receta in recetas)
                    {
                        if (receta.IdUsuario == usuarioIndividual.IdUsuario)
                        {
                            encontrados.Add(receta);
                        }

                    }

                    return encontrados.ToArray<Receta>();
                }
            }
            return NotFound();
        }

        // GET: api/poringredientefaltante/ingrediente
        [HttpGet("buscar/poringredientefaltante/{ingrediente}")]
        public async Task<ActionResult<IEnumerable<Receta>>> GetRecetaPorIngredienteFaltante(string ingrediente)
        {
            if (_context.Recetas == null)
            {
                return NotFound();
            }
            List<Receta> encontrados = await _context.Recetas.ToListAsync();

            var ingredientes = await _context.Ingredientes.ToListAsync();

            foreach (var ingredienteIndividual in ingredientes)
            {
                if (ingredienteIndividual.Nombre == ingrediente)
                {
                    var utilizados = await _context.Utilizados.ToListAsync();
                    foreach (var utilizado in utilizados)
                    {
                        var recetas = await _context.Recetas.ToListAsync();
                        foreach (var receta in recetas)
                        {
                            if (utilizado.IdReceta == receta.IdReceta && utilizado.IdIngrediente == ingredienteIndividual.IdIngrediente)
                            {
                                encontrados.Remove(receta);
                            }

                        }

                        
                    }
                }return encontrados.ToArray<Receta>();
            }
            return NotFound();
        }


        // PUT: api/Recetas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReceta(int id, Receta receta)
        {
            if (id != receta.IdReceta)
            {
                return BadRequest();
            }

            _context.Entry(receta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecetaExists(id))
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

        // POST: api/Recetas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Receta>> PostReceta(Receta receta)
        {
          if (_context.Recetas == null)
          {
              return Problem("Entity set 'ApplicacionesDistribuidasContext.Recetas'  is null.");
          }
            _context.Recetas.Add(receta);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReceta", new { id = receta.IdReceta }, receta);
        }

        // DELETE: api/Recetas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReceta(int id)
        {
            if (_context.Recetas == null)
            {
                return NotFound();
            }
            var receta = await _context.Recetas.FindAsync(id);
            if (receta == null)
            {
                return NotFound();
            }

            _context.Recetas.Remove(receta);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RecetaExists(int id)
        {
            return (_context.Recetas?.Any(e => e.IdReceta == id)).GetValueOrDefault();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppDistribuidas.Models;
using SendGrid.Helpers.Mail;
using SendGrid;
using Microsoft.AspNetCore.Http.HttpResults;

namespace AppDistribuidas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly ApplicacionesDistribuidasContext _context;

        public UsuariosController(ApplicacionesDistribuidasContext context)
        {
            _context = context;
        }

        // GET: api/Usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
          if (_context.Usuarios == null)
          {
              return NotFound();
          }
            return await _context.Usuarios.ToListAsync();
        }

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
          if (_context.Usuarios == null)
          {
              return NotFound();
          }
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        // PUT: api/Usuarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, Usuario usuario)
        {
            if (id != usuario.IdUsuario)
            {
                return BadRequest();
            }

            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
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

        // POST: api/Usuarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
        {
          if (_context.Usuarios == null)
          {
              return Problem("Entity set 'ApplicacionesDistribuidasContext.Usuarios'  is null.");
          }

            var usuarioExistente = await _context.Usuarios.FirstOrDefaultAsync(u => u.Mail == usuario.Mail);
            var usuarioExistente2 = await _context.Usuarios.FirstOrDefaultAsync(u => u.Nickname == usuario.Nickname);

            if (usuarioExistente != null)
            {
                if(usuarioExistente.Habilitado == "Si")
                    return BadRequest("Un usuario con ese email ya existe y esta habilitado, redirigir a recupero de contrasena");
                else
                    return BadRequest("Un usuario con ese email ya existe");
            }
            if (usuarioExistente2 != null)
            {
                return BadRequest($"Un usuario con ese alias ya existe. Sugerencia: {usuario.Nickname + new Random().Next(10000)}");
            }




            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            await RegistracionExitosa(usuario.Mail, usuario.Nombre);
            


            return CreatedAtAction("GetUsuario", new { id = usuario.IdUsuario }, usuario);
        }

        [HttpPost("Habilitar")]
        public async Task<ActionResult<Usuario>> PostHabilitarUsuario(string usuarioMail)
        {
            if (_context.Usuarios == null)
            {
                return Problem("Entity set 'ApplicacionesDistribuidasContext.Usuarios'  is null.");
            }
            var usuarioExistente = await _context.Usuarios.FirstOrDefaultAsync(u => u.Mail == usuarioMail);
            if (usuarioExistente == null )
            {
                return BadRequest("El usuario no existe");
            }
            else
            {
                usuarioExistente.Habilitado = "Si";
                _context.Usuarios.Update(usuarioExistente);
                await _context.SaveChangesAsync();
            }
            return Ok(usuarioExistente);
        }
        [HttpPost("recuperarPasssword")]
        public async Task<ActionResult<Usuario>> PostrecuperoContrasena(string usuarioMail)
        {
            if (_context.Usuarios == null)
            {
                return Problem("Entity set 'ApplicacionesDistribuidasContext.Usuarios'  is null.");
            }
            var usuarioExistente = await _context.Usuarios.FirstOrDefaultAsync(u => u.Mail == usuarioMail);
            if (usuarioExistente == null )
            {
                return BadRequest("El usuario no existe");
            }
            
            var codigo = new Random().Next(1000000);
            usuarioExistente.Token = codigo.ToString();
            _context.Usuarios.Update(usuarioExistente);
            await _context.SaveChangesAsync();
            await RecuperocontrasenaEmail(usuarioExistente.Mail, usuarioExistente.Nickname, codigo.ToString());
            return Ok(usuarioExistente);
        }

        [HttpPost("cambioPassword")]
        public async Task<ActionResult<Usuario>> PostrecuperoContrasena(string usuarioMail, string codigo, string passwordNuevo)
        {
            if (_context.Usuarios == null)
            {
                return Problem("Entity set 'ApplicacionesDistribuidasContext.Usuarios'  is null.");
            }
            var usuarioExistente = await _context.Usuarios.FirstOrDefaultAsync(u => u.Mail == usuarioMail);
            if (usuarioExistente == null)
            {
                return BadRequest("El usuario no existe");
            }

            if(usuarioExistente.Token != codigo)
            {
                return BadRequest("Codigo erroneo");
            }
            usuarioExistente.Password = passwordNuevo;
            _context.Usuarios.Update(usuarioExistente);
            await _context.SaveChangesAsync();
            //await RecuperocontrasenaEmail(usuarioExistente.Mail, usuarioExistente.Nickname, codigo.ToString());
            return Ok(usuarioExistente);
        }
        private async Task RegistracionExitosa(string useremail, string username)
        {
            var apiKey = "replace here";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("chuerta@uade.edu.ar", "Bonaapetit Registracion");
            var subject = "Te registraste a bonappetit!!";
            var to = new EmailAddress(useremail,username);
            var plainTextContent = "Registracion exitosa!";
            var htmlContent = "<strong>por favor ingres al sitio y completa los datos de tu registro</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }

        private async Task RecuperocontrasenaEmail(string useremail, string username, string codigo)
        {
            var apiKey = "replace here";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("chuerta@uade.edu.ar", "Bonaapetit Recupero de contrasena");
            var subject = "Recupero de contrasena bonappetit";
            var to = new EmailAddress(useremail, username);
            var plainTextContent = $"recupera tu contrasena con el codigo {codigo}";
            var htmlContent = $"<strong>recupera tu contrasena con el codigo {codigo}</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }

        // DELETE: api/Usuarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            if (_context.Usuarios == null)
            {
                return NotFound();
            }
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsuarioExists(int id)
        {
            return (_context.Usuarios?.Any(e => e.IdUsuario == id)).GetValueOrDefault();
        }
    }
}

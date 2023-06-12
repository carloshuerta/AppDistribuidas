using AppDistribuidas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Mail;
using SendGrid;

namespace AppDistribuidas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {

        private readonly ApplicacionesDistribuidasContext _context;
        public LoginController(ApplicacionesDistribuidasContext context)
        {
            _context = context;
        }
        // POST: api/Usuarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("login")]
        public async Task<ActionResult<Usuario>> Login(string userId, string password)
        {
            if (_context.Usuarios == null)
            {
                return NotFound();
            }
            var usuarios = await _context.Usuarios.ToListAsync();
            foreach(var usuario in usuarios)
            {
                if (usuario.Mail == userId && usuario.Password == password)
                {
                    return usuario;
                }
                if (usuario.Nickname == userId && usuario.Password == password)
                {
                    return usuario;
                }
            }

            return NotFound();

        }
        
        [HttpPost("sendmail")]
        public async Task<ActionResult<Usuario>> RegistracionExitosa()
        {
                var apiKey = "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";
                var client = new SendGridClient(apiKey);
                var from = new EmailAddress("chuerta@uade.edu.ar", "Example User");
                var subject = "Sending with SendGrid is Fun";
                var to = new EmailAddress("carlos.huerta@gmail.com", "Example User");
                var plainTextContent = "and easy to do anywhere, even with C#";
                var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                var response = await client.SendEmailAsync(msg);
            

            return Ok();

        }
    }
}

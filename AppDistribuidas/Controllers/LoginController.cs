using AppDistribuidas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        [HttpPost]
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
    }
}

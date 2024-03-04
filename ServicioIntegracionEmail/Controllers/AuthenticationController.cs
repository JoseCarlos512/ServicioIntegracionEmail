using Microsoft.AspNetCore.Mvc;
using ServicioIntegracionEmail.Models.Entity;
using ServicioIntegracionEmail.Models.Services;

namespace ServicioIntegracionEmail.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public AuthenticationController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost]
        public async Task<IActionResult> PostAuth(Authentication auth)
        {
            string[] lAuthResult = await _usuarioService.Authenticate(auth);

            if (lAuthResult.Length == 0)
            {
                return Ok(new { message = "Intente nuevamente" });
            }
            if (!string.IsNullOrEmpty(lAuthResult[0]))
            {
                return Ok(new { message = lAuthResult[0] });
            }
            if (string.IsNullOrEmpty(lAuthResult[1]))
            {
                return Ok(new { message = "Intente nuevamente" });
            }
            return Ok(new { token = lAuthResult[1] });
        }
    }
}

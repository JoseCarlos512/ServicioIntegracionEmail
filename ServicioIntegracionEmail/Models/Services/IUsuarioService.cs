using ServicioIntegracionEmail.Models.Entity;

namespace ServicioIntegracionEmail.Models.Services
{
    public interface IUsuarioService
    {
        Task<string[]> Authenticate(Authentication auth);
    }
}

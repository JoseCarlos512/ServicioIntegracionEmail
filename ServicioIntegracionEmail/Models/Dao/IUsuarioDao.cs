using ServicioIntegracionEmail.Models.Entity;

namespace ServicioIntegracionEmail.Models.Dao
{
    public interface IUsuarioDao
    {
        Task<Usuario> ObtenerUsuarioPorNombre(string nombreUsuario);
    }
}

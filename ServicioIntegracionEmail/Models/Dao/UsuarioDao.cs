using Microsoft.EntityFrameworkCore;
using ServicioIntegracionEmail.Data;
using ServicioIntegracionEmail.Models.Entity;

namespace ServicioIntegracionEmail.Models.Dao
{
    public class UsuarioDao : IUsuarioDao
    {
        private readonly IntegracionEmailContext _context;

        public UsuarioDao(IntegracionEmailContext context)
        {
            _context = context;
        }

        public async Task<Usuario> ObtenerUsuarioPorNombre(string nombreUsuario)
        {
            // Cambia la consulta según tu procedimiento almacenado o consulta SQL
            var query = await _context.Usuario.FromSqlInterpolated($"EXEC ObtenerUsuarioPorNombre {nombreUsuario}").ToListAsync();

            // Realiza la composición en el lado del cliente llamando a AsEnumerable
            return query.FirstOrDefault();
        }
    }
}

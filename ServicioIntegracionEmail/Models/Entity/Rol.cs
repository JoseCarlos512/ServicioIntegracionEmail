using System.ComponentModel.DataAnnotations;

namespace ServicioIntegracionEmail.Models.Entity
{
    public class Rol
    {
        [Key]
        public int idrol { get; set; }
        public string nombre { get; set; } = null!;
        public string descripcion { get; set; } = null!;
        public bool activo { get; set; }
    }
}

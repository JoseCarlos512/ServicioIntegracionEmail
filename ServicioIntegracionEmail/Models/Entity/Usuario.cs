using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServicioIntegracionEmail.Models.Entity
{
    public class Usuario
    {
        [Key]
        public int idusuario { get; set; }
        public string username { get; set; } = null!;
        public byte[] password { get; set; } = null!;
        public string nombre { get; set; } = null!;
        public string apellido { get; set; } = null!;
        public string email { get; set; } = null!;
        public bool activo { get; set; }
    }
}

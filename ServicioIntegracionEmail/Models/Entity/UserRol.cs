using System.ComponentModel.DataAnnotations.Schema;

namespace ServicioIntegracionEmail.Models.Entity
{
    [Table("rol_user")]
    public class UserRol
    {
        public int idusuario { get; set; }
        public int idrol { get; set; }
        public bool activo { get; set; }

       
    }

}

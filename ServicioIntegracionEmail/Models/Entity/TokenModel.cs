namespace ServicioIntegracionEmail.Models.Entity
{
    public class TokenModel
    {
        public int iduser { get; set; }
        public string username { get; set; } = null!;
        public string nombre { get; set; } = null!;
        public string apellido { get; set; } = null!;
        public string email { get; set; } = null!;
        public int idrol { get; set; }
        public string rolname { get; set; } = null!;
    }
}

using System.Security.Cryptography;
using System.Text;
using ServicioIntegracionEmail.Auth;
using ServicioIntegracionEmail.Models.Dao;
using ServicioIntegracionEmail.Models.Entity;

namespace ServicioIntegracionEmail.Models.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioDao _usuarioDao;
        private readonly IConfiguration _config;
        private readonly TokenAuth _tokenAuth;

        public UsuarioService(IUsuarioDao usuarioDao, IConfiguration configuration, TokenAuth tokenAuth)
        {
            _usuarioDao = usuarioDao;
            _config = configuration;
            _tokenAuth = tokenAuth;
        }

        public async Task<string[]> Authenticate(Authentication auth)
        {
            String[] lresult = new string[2];

            string userMater = _config["MasterUser"];
            string passMater = _config["MasterPassword"];
            string emailMater = _config["MasterEmail"];

            if (auth.username == userMater && auth.password == passMater)
            {
                var _tokenModel = new TokenModel();

                var user = await _usuarioDao.ObtenerUsuarioPorNombre(auth.username);
                if (user == null || !user.activo)
                {
                    lresult[0] = "Usuario no habilitado";
                    return lresult;
                }

                _tokenModel.username = userMater;
                _tokenModel.nombre = "Master";
                _tokenModel.apellido = "";
                _tokenModel.email = emailMater;
                lresult[1] = _tokenAuth.GenerateToken(_tokenModel);

                return lresult;
            }
            else
            {
                byte[] hashedPassword = (SHA256.Create()).ComputeHash(Encoding.UTF8.GetBytes(auth.password));
                var user = await _usuarioDao.ObtenerUsuarioPorNombre(auth.username);

                if (user == null || !user.activo)
                {
                    lresult[0] = "Usuario no habilitado";
                    return lresult;
                }

                if (Convert.ToBase64String(user.password) != Convert.ToBase64String(hashedPassword))
                {
                    lresult[0] = "Contraseña incorrecta";
                    return lresult;
                }

                lresult[1] = _tokenAuth.GenerateToken(new TokenModel
                {
                    username = user.username,
                    nombre = user.nombre,
                    apellido = user.apellido,
                    email = user.email,
                    idrol = 1, // Ajusta según tu lógica
                    rolname = "administrador", // Ajusta según tu lógica
                });
            }

            return lresult;
        }

    }
}

using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using ServicioIntegracionEmail.Helpers;
using ServicioIntegracionEmail.Models.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ServicioIntegracionEmail.Auth
{
    public class TokenAuth
    {
        private readonly IConfiguration _config;

        public TokenAuth(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateToken(TokenModel tokenModel)
        {
            DateTime expire_token = UtilityHelper.ExpireToken(_config["ExpiresToken"]);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    /*[0]*/new Claim(ClaimTypes.Name, tokenModel.username),
                    /*[2]*/new Claim(ClaimTypes.Role, tokenModel.idrol.ToString()),
                    /*[2]*/new Claim(ClaimTypes.Email, tokenModel.email),
                    /*[2]*/new Claim(ClaimTypes.UserData, JsonConvert.SerializeObject(new{
                        Usuario = tokenModel.username,
                        Nombre = tokenModel.nombre,
                        Rol = tokenModel.idrol,
                        RolDesc = tokenModel.rolname
                    }))
                }),
                Expires = expire_token,
                SigningCredentials = credentials
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

using Modelos;
using Logica;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/usuarios")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly ILogicaUsuarios _logicaUsuarios;
        private readonly IConfiguration _configuration;

        public UsuarioController(ILogicaUsuarios logicaUsuarios,
            IConfiguration configuration)
        {
            _logicaUsuarios = logicaUsuarios;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("crear")]
        public async Task<ObtenerUsuario> Crear([FromBody] CrearUsuario usuario)
        {
            return await _logicaUsuarios.CrearUsuario(usuario);
        }

        [HttpPost]
        [Route("actualizar")]
        public async Task<ObtenerUsuario> Actualizar([FromBody] ActualizarUsuario usuario)
        {
            return await _logicaUsuarios.ActualizarUsuario(usuario);
        }

        [HttpPost]
        [Route("eliminar")]
        public async Task Eliminar([FromQuery] int codigoUsuario)
        {
            await _logicaUsuarios.EliminarUsuario(codigoUsuario);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody]LoginUsuario login)
        {
            IActionResult response = Unauthorized();
            var user = await _logicaUsuarios.Loguin(login);

            if (user != null)
            {
                var tokenString = GenerateJSONWebToken(user);
                response = Ok(new { token = tokenString });
            }

            return response;
        }

        private string GenerateJSONWebToken(ObtenerUsuario userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim("Login", JsonConvert.SerializeObject(userInfo))
            };

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                _configuration["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

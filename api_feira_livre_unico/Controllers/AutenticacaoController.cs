using Api_Business.Interface;
using Api_Entities.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api_feira_livre_unico.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacaoController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ILogger _Logger;
        private readonly IService _consulta;


        public AutenticacaoController(IConfiguration Configuration, ILogger<AutenticacaoController> logger, IService consulta)
        {
            _config = Configuration;
            _Logger = logger;
            _consulta = consulta;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UsuarioDTO loginDetalhes)
        {
            bool resultado = await ValidarUsuario(loginDetalhes);
            if (resultado)
            {
                _Logger.LogInformation($@"Usuario logado com sucesso :{loginDetalhes.Usuario}");
                var tokenString = GerarTokenJWT();
                return Ok(new { token = tokenString });
            }
            else
            {
                return Unauthorized();
            }
        }

        private string GerarTokenJWT()
        {
            _Logger.LogInformation("Gerando JWT");

            var issuer = _config["Jwt:Issuer"];
            var audience = _config["Jwt:Audience"];
            _ = DateTime.Now.AddMinutes(120);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(issuer: issuer, audience: audience, expires: DateTime.Now.AddMinutes(120), signingCredentials: credentials);
            var tokenHandler = new JwtSecurityTokenHandler();
            var stringToken = tokenHandler.WriteToken(token);

            return stringToken;
        }
        private async Task<bool> ValidarUsuario(UsuarioDTO loginDetalhes)
        {
            _Logger.LogInformation($@"Validando usuario {loginDetalhes.Usuario},{loginDetalhes.Senha}");
            var consulta = await _consulta.Usuario(loginDetalhes);
            return consulta;
        }
    }
}

using System.Security.Cryptography;
using System.Net.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;


namespace todo.api.Services.Auth
{
    public class JwtService : IJwtService
    {

        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration=configuration;
        }

        
        public string GenerarToken(IEnumerable<Claim> claims)
        {
            var clave = _configuration["Jwt:Key"] ?? throw new InvalidOperationException("Jwt no configurada");

            var claveBite = Encoding.UTF8.GetBytes(clave);

            var securitykey = new SymmetricSecurityKey(claveBite);


            var credentials  = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);


            // Queda pendiente la implementacion del token 
            var issuer = _configuration["Jwt:issuer"];
            var audience = _configuration["Jwt:Audience"];

            var expirationMinutes = int.Parse(
                _configuration["Jwt:ExpirationMinutes"] ?? "60");

              var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expirationMinutes),
                signingCredentials: credentials
            );

            var tokenHander = new JwtSecurityTokenHandler();

            return tokenHander.WriteToken(token);
        }
    }
}
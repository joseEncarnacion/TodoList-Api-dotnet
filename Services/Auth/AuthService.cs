using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using todo.api.DTOs.Auth;
using todo.api.Models;

namespace todo.api.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<Usuario> _userManager;

        public AuthService(UserManager<Usuario> userManager)
        {
            _userManager = userManager;
        }

        public async Task<LoginResponseDTO?> Login(LoginRequestDTO dto)
        {
            var usuario = await _userManager.FindByNameAsync(dto.NombreUsuario);

            if (usuario == null) return null;

            var PasswordCorrecta = await _userManager.CheckPasswordAsync(usuario, dto.Password);

            if (!PasswordCorrecta)
            {
                return null;    
            }
            
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),

                new Claim(ClaimTypes.Name, usuario.UserName ?? string.Empty),

                new Claim(ClaimTypes.Email, usuario.Email ?? string.Empty),
            };

            var respuesta = new LoginResponseDTO
            {
                Token = "Token"
            };


            return respuesta;
        }

        public async Task<IdentityResult> Registrar(RegisterRequestDTO dto)
        {
         
            var usuario = new Usuario
            {
                NombreUsuario = dto.NombreUsuario,
                UserName = dto.NombreUsuario,
                Email = dto.Email
            };

            var resultado = await _userManager.CreateAsync(usuario, dto.Password);

            return resultado;
        }

       
    }
}
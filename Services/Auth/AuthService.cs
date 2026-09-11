using System;
using System.Collections.Generic;
using System.Linq;
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
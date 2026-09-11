using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using todo.api.DTOs.Auth;

namespace todo.api.Services.Auth
{
    public interface IAuthService
    {
        Task<IdentityResult> Registrar(RegisterRequestDTO dto);
    }
}
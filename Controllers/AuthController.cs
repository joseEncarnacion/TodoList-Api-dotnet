using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using todo.api.DTOs.Auth;
using todo.api.Services.Auth;

namespace todo.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
       private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
          _authService = authService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Registrar(RegisterRequestDTO dto)
        {
            var resultado  = await _authService.Registrar(dto);

            if (resultado.Succeeded)
            {
                return Ok();
            }

            return BadRequest(resultado.Errors);
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails),StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login(LoginRequestDTO dto)
        {
            var resultado =  await _authService.Login(dto);

            if (resultado == null)
            {
                return Unauthorized();
            }

            return Ok(resultado);

        }

    }
}
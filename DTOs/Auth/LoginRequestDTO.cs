using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace todo.api.DTOs.Auth
{
    public class LoginRequestDTO
    {
        [Required]
        public string NombreUsuario { get; set; }= string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
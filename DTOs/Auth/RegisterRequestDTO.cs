using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace todo.api.DTOs.Auth
{
    public class RegisterRequestDTO
    {
        [Required]
        [StringLength(50)]
        public string NombreUsuario { get; set; } = string.Empty;
       
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }= string.Empty;
    }
}
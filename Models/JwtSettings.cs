using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace todo.api.Models
{
    public class JwtSettings
    {
        [Required]
        public string Key { get; set; } = string.Empty;

        [Required]
        public string Issuer { get; set; } = string.Empty ;

        [Required]
        public string Audience { get; set; } = string.Empty;

        [Required]
        public int ExpirationMinutes { get; set; } 

    }
}
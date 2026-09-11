using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace todo.api.Models
{
    public class Usuario: IdentityUser<int>
    {
        // public int UsuarioID { get; set; }  // el identity asume a Usuario.Id y no cambia como se ha implementado en tareas y otras clases
        public string NombreUsuario { get; set; } = string.Empty;

        public ICollection<Tarea> Tareas { get; set; } = new List<Tarea>();
        
    }
}
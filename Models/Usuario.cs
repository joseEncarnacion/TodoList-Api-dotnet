using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace todo.api.Models
{
    public class Usuario
    {
        public int UsuarioID { get; set; }  
        public string NombreUsuario { get; set; } = string.Empty;

        public ICollection<Tarea> Tareas { get; set; } = new List<Tarea>();
        
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace todo.api.Models
{
    public class Tarea
    {
        public int TareaID { get; set; }
        public int UsuarioID { get; set; }
        public int EstadoID { get; set; }
        public int PrioridadID { get; set; }

        public string Descripcion { get; set; } = string.Empty;
        public DateTime fechaCreacion { get; set; } 

        public Usuario Usuario { get; set; } = null!;
        public Estado Estado { get; set; } = null!;
        public Prioridad Prioridad { get; set; } = null!;
        
    }
    
}
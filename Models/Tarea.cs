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

        public string Descripcion { get; set; }
        public DateTime fechaCreacion { get; set; }

        public Usuario Usuario { get; set; }
        public Estado Estado { get; set; }
        public Prioridad Prioridad { get; set; }
        
    }
    
}
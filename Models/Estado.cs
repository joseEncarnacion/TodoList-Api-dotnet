using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace todo.api.Models
{
    public class Estado
    {
        public int EstadoID { get; set; }
        public string NombreEstado { get; set; }

        public ICollection<Tarea> Tareas { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace todo.api.Models
{
    public class Prioridad
    {
        public int PrioridadID { get; set; }
        public string NombrePrioridad { get; set; } = string.Empty;

        public ICollection<Tarea> Tareas { get; set; } = new List<Tarea>();
    }
}
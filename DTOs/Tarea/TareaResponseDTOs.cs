using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace todo.api.DTOs.Tarea
{
    public class TareaResponseDTOs
    {
        public int TareaID { get; set; }
        public string Descripcion { get; set; } = "";
        public DateTime FechaCreacion { get; set; }
        // public int Progreso { get; set; }
        public string Usuario { get; set; } = "";
        public string Estado { get; set; }  = "";
        public string Prioridad { get; set; } = "";
    }
}
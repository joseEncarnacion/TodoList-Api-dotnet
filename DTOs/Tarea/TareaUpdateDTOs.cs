using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace todo.api.DTOs.Tarea
{
    public class TareaUpdateDTOs
    {
    
        
        [Range(1, int.MaxValue)]
        public int EstadoID { get; set; }
        [Range(1, int.MaxValue)]
        public int PrioridadID { get; set; }
        [Required]
        [StringLength(300)]
        public string Descripcion { get; set; } = "";
    }
}
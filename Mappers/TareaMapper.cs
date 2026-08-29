using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using todo.api.DTOs.Tarea;
using todo.api.Models;

namespace todo.api.Mappers
{
    public static class TareaMapper
    {
        public static Tarea ToEntity(TareaCreateDTOs dto)
        {
            var tarea = new Tarea{
                UsuarioID = dto.UsuarioID,
                EstadoID = dto.EstadoID,
                PrioridadID = dto.PrioridadID,
                Descripcion = dto.Descripcion,
                fechaCreacion = DateTime.Now
            };

            return tarea;   

        }


        // public static TareaResponseDTOs ToResponse(Tarea tarea)
        // {
        //     var response = new TareaResponseDTOs
        //     {
        //         TareaID = tarea.TareaID,
        //         Descripcion = tarea.Descripcion,
        //         FechaCreacion = tarea.fechaCreacion,

        //         Usuario = tarea.Usuario.NombreUsuario,
        //         Estado = tarea.Estado.NombreEstado,
        //         Prioridad = tarea.Prioridad.NombrePrioridad
        //     };

        //     return response;
        // }


        public static void UpdateEntity(Tarea tarea, TareaUpdateDTOs dto)
        {
            tarea.EstadoID =  dto.EstadoID;
            tarea.PrioridadID = dto.PrioridadID;
            tarea.Descripcion = dto.Descripcion;
        }


        public static Expression<Func<Tarea, TareaResponseDTOs>> ToResponse()
        {
            return t => new TareaResponseDTOs
            {
                TareaID = t.TareaID,
                Descripcion = t.Descripcion,
                FechaCreacion = t.fechaCreacion,

                Usuario = t.Usuario.NombreUsuario,
                Estado = t.Estado.NombreEstado,
                Prioridad = t.Prioridad.NombrePrioridad
            };
        }
    }
}
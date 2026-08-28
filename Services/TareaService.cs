using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using todo.api.Data;
using todo.api.DTOs.Tarea;
using todo.api.Mappers;
using todo.api.Models;

namespace todo.api.Services
{
    public class TareaService : ITareaService
    {
        private readonly AppDbContext _context;

        public TareaService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<TareaResponseDTOs?> Actualizar(int id, TareaUpdateDTOs dto)
        {
           var actualizarTarea  = await _context.Tareas
           .FirstOrDefaultAsync(t => t.TareaID == id);

           if (actualizarTarea == null) return null;

           TareaMapper.UpdateEntity(actualizarTarea, dto); 

           await _context.SaveChangesAsync();

           
            var tareaCtualizada = await _context.Tareas
            .Where(t => t.TareaID ==id)
            .Select(t => new TareaResponseDTOs
            {
                TareaID = t.TareaID,
                Descripcion = t.Descripcion,
                FechaCreacion = t.fechaCreacion,

                Usuario = t.Usuario.NombreUsuario,
                Estado = t.Estado.NombreEstado,
                Prioridad = t.Prioridad.NombrePrioridad
            }).FirstOrDefaultAsync();

            return tareaCtualizada;



        }

        public async Task<TareaResponseDTOs> Crear(TareaCreateDTOs dto)
        {
            var tarea = TareaMapper.ToEntity(dto);

            _context.Tareas.Add(tarea);
            await _context.SaveChangesAsync();

            var tareasGuardada = await _context.Tareas
            .Where(t => t.TareaID == tarea.TareaID)
            .Select(t => new TareaResponseDTOs
            {
                TareaID = t.TareaID,
                Descripcion = t.Descripcion,
                FechaCreacion = t.fechaCreacion,

                Usuario = t.Usuario.NombreUsuario,
                Estado = t.Estado.NombreEstado,
                Prioridad = t.Prioridad.NombrePrioridad
            }).FirstOrDefaultAsync();

            return tareasGuardada!;
        }

        public async Task<bool> Eliminar(int id)
        {
            var tarea =  await _context.Tareas
            .FirstOrDefaultAsync(t => t.TareaID == id);

            if(tarea == null) return false;

            _context.Tareas.Remove(tarea);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<TareaResponseDTOs>> Filtrar(int? estadoID, int? prioridadID)
        {
            var query =  _context.Tareas.AsQueryable();

            if (estadoID.HasValue)
            {
                query = query.Where(t => t.EstadoID == estadoID.Value);
            }

            if (prioridadID.HasValue)
            {
                query = query.Where(t => t.PrioridadID == prioridadID.Value);
            }

            var response = await query
            .Select(t => new TareaResponseDTOs
            {
                TareaID = t.TareaID,
                Descripcion = t.Descripcion,
                FechaCreacion = t.fechaCreacion,

                Usuario = t.Usuario.NombreUsuario,
                Estado = t.Estado.NombreEstado,
                Prioridad = t.Prioridad.NombrePrioridad

            }).ToListAsync();

            return response;
        }

        public async Task<List<TareaResponseDTOs>> Listar()
        {
            return await _context.Tareas
            .Select(t => new TareaResponseDTOs
            {
                TareaID = t.TareaID,
                Descripcion = t.Descripcion,
                FechaCreacion = t.fechaCreacion,
                Usuario = t.Usuario.NombreUsuario,
                Estado = t.Estado.NombreEstado,
                Prioridad = t.Prioridad.NombrePrioridad
            }).ToListAsync();
        }

        public async Task<TareaResponseDTOs?> ObtenerPorID(int id)
        {
            return await _context.Tareas
            .Where(t => t.TareaID == id)
            .Select(t => new TareaResponseDTOs
            {
                TareaID = t.TareaID,
                Descripcion = t.Descripcion,
                FechaCreacion = t.fechaCreacion,

                Usuario = t.Usuario.NombreUsuario,
                Estado = t.Estado.NombreEstado,
                Prioridad = t.Prioridad.NombrePrioridad

            }).FirstOrDefaultAsync();

            

            
        }
    }
}
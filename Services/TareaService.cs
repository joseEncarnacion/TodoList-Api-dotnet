using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using todo.api.Data;
using todo.api.DTOs.Tarea;
using todo.api.Mappers;
using todo.api.Models;
using todo.api.Validators;

namespace todo.api.Services
{
    public class TareaService : ITareaService
    {
        private readonly AppDbContext _context;
        private readonly TareaValidator _validator;
        private readonly ILogger<TareaService> _logger;

        public TareaService(AppDbContext context, TareaValidator validator, ILogger<TareaService> logger)
        {
            _context = context;
            _validator = validator;
            _logger = logger;
        }
        public async Task<TareaResponseDTOs?> Actualizar(int id, TareaUpdateDTOs dto)
        {
           var actualizarTarea  = await _context.Tareas
           .FirstOrDefaultAsync(t => t.TareaID == id);

           if (actualizarTarea == null)
            {
                _logger.LogWarning("No se encontró la tarea {TareaID} para actualizar",id);
                return null;
            }

           await _validator.ValidarActualizar(dto);

           TareaMapper.UpdateEntity(actualizarTarea, dto); 

           await _context.SaveChangesAsync();

            _logger.LogInformation("La tarea {TareaId} fue actualizada correctamente",id);

           
            var tareaActualizada = await _context.Tareas
            .Where(t => t.TareaID ==id)
            .Select(TareaMapper.ToResponse())
            .FirstOrDefaultAsync();

            return tareaActualizada;

        }

        public async Task<TareaResponseDTOs> Crear(TareaCreateDTOs dto)
        {
            
            await _validator.ValidarCrear(dto);
            
            var tarea = TareaMapper.ToEntity(dto);

            _context.Tareas.Add(tarea);
            await _context.SaveChangesAsync();

           _logger.LogInformation("La tarea {TareaId} fue creada correctamente",tarea.TareaID);

            var tareaGuardada = await _context.Tareas
            .Where(t => t.TareaID == tarea.TareaID)
            .Select(TareaMapper.ToResponse())
            .FirstOrDefaultAsync();

            return tareaGuardada!;
        }

        public async Task<bool> Eliminar(int id)
        {
            var tarea =  await _context.Tareas
            .FirstOrDefaultAsync(t => t.TareaID == id);

            if(tarea == null) {

                _logger.LogWarning("No se encontró la tarea {TareaID} para eliminar",id);
                return false;

            }


            _context.Tareas.Remove(tarea);
            await _context.SaveChangesAsync();

            _logger.LogInformation("La tarea {TareaId} fue eliminada correctamente",id);

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
            .Select(TareaMapper.ToResponse())
            .ToListAsync();



            return response;
        }

        public async Task<List<TareaResponseDTOs>> Listar()
        {

            var tareas = await _context.Tareas
            .Select(TareaMapper.ToResponse()).ToListAsync();

            _logger.LogInformation("Se obtuvieron {CantidadTareas} tareas", tareas.Count);
            return tareas;

        }

        public async Task<TareaResponseDTOs?> ObtenerPorID(int id)
        {
        
            var tarea =  await _context.Tareas
            .Where(t => t.TareaID == id)
            .Select(TareaMapper.ToResponse())
            .FirstOrDefaultAsync();

            if (tarea == null)
            {
             _logger.LogWarning("No se encontró la tarea {TareaId} solicitada",id);

             return null;
                
            }

             _logger.LogInformation("Se obtuvo correctamente la tarea {TareaId}",id);

            return tarea;


        }
    }
}
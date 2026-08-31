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

        public TareaService(AppDbContext context, TareaValidator validator)
        {
            _context = context;
            _validator = validator;
        }
        public async Task<TareaResponseDTOs?> Actualizar(int id, TareaUpdateDTOs dto)
        {
           var actualizarTarea  = await _context.Tareas
           .FirstOrDefaultAsync(t => t.TareaID == id);

           if (actualizarTarea == null) return null;

           await _validator.ValidarActualizar(dto);

           TareaMapper.UpdateEntity(actualizarTarea, dto); 

           await _context.SaveChangesAsync();

           
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
            .Select(TareaMapper.ToResponse())
            .ToListAsync();

            return response;
        }

        public async Task<List<TareaResponseDTOs>> Listar()
        {
            return await _context.Tareas
            .Select(TareaMapper.ToResponse()).ToListAsync();
        }

        public async Task<TareaResponseDTOs?> ObtenerPorID(int id)
        {
            return await _context.Tareas
            .Where(t => t.TareaID == id)
            .Select(TareaMapper.ToResponse())
            .FirstOrDefaultAsync();

        }
    }
}
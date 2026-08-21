using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using todo.api.Data;
using todo.api.DTOs.Tarea;
using todo.api.Models;

namespace todo.api.Controllers
{
    [Route("[controller]")]
    public class TareaController : Controller
    {
        private readonly AppDbContext _context;

        public TareaController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]   
        public async Task<IActionResult> ListarTareas()
        {
            
            // var tarea = await _context.Tareas
            // .Include(t => t.Usuario)
            // .Include(t => t.Estado)
            // .Include(t => t.Prioridad)
            // .ToListAsync();

            var response = await _context.Tareas
            .Select(t => new TareaResponseDTOs
            {
                TareaID = t.TareaID,
                Descripcion = t.Descripcion,
                FechaCreacion = t.fechaCreacion,
                Usuario = t.Usuario.NombreUsuario,
                Estado = t.Estado.NombreEstado,
                Prioridad = t.Prioridad.NombrePrioridad

            }).ToListAsync();

            return Ok(response);

            
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorID(int id)
        {
            var response = await _context.Tareas
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

            if(response == null) return NotFound();

            return Ok(response);    
        }

        [HttpPost]
        public async Task<IActionResult> Crear(TareaCreateDTOs  dto)
        {

            if(!ModelState.IsValid) return BadRequest(ModelState);

            //Crear la tarea a partir de DTO
           var tarea = new Tarea{
            UsuarioID = dto.UsuarioID,
            EstadoID = dto.EstadoID,
            PrioridadID = dto.PrioridadID,
            Descripcion = dto.Descripcion,
            fechaCreacion = DateTime.Now

           };
           
           //Agregar entidad
           _context.Tareas.Add(tarea);
           //Guardar
           await _context.SaveChangesAsync();

           //Obtener producto con su relacion 
           var tareaGuardada = await _context.Tareas
             .Include(t => t.Usuario)
             .Include(t => t.Estado)
             .Include(t => t.Prioridad)
             .FirstOrDefaultAsync(t => t.TareaID == tarea.TareaID);

             if (tareaGuardada == null) return NotFound();

             //Entidad a inseretar en eDTO

             var response = new TareaResponseDTOs
             {
                 TareaID  = tareaGuardada.TareaID,
                 Descripcion = tareaGuardada.Descripcion,
                 FechaCreacion = tareaGuardada.fechaCreacion,

                 Usuario = tareaGuardada.Usuario.NombreUsuario,
                 Estado = tareaGuardada.Estado.NombreEstado,
                 Prioridad = tareaGuardada.Prioridad.NombrePrioridad
             };       


             return CreatedAtAction(nameof(ObtenerPorID), new {id= tarea.TareaID}, response);     




           // devolver DTO
        //    return Ok(response);

           
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, TareaUpdateDTOs dto)
        {
            // 1. Buscar entidad existente
            var tarea = await _context.Tareas
            .FirstOrDefaultAsync(t => t.TareaID == id);

            //Validar si exite
            if(tarea == null) return NotFound();

            tarea.EstadoID = dto.EstadoID;
            tarea.PrioridadID = dto.PrioridadID;
            tarea.Descripcion = dto.Descripcion;

            await _context.SaveChangesAsync();

            //hacemos una consulta para devolver la proyeccion y no la entidad

            var respuesta = await _context.Tareas
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

            return Ok(respuesta);


            // var tarea = await _context.Tareas.FindAsync(id);

            // if (tarea == null) return NotFound();

            // tarea.EstadoID = dto.EstadoID;
            // tarea.PrioridadID = dto.PrioridadID;
            // tarea.Descripcion = dto.Descripcion;

            // await _context.SaveChangesAsync();

            // return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var tarea = await _context.Tareas
            .FirstOrDefaultAsync(t => t.TareaID == id);

            if(tarea == null) return NotFound();

            // Eliminar tarea
            _context.Tareas.Remove(tarea);
            await _context.SaveChangesAsync();

            return NoContent();
        }


    }
}
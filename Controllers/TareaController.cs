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
using todo.api.Mappers;
using todo.api.Models;
using todo.api.Services;

namespace todo.api.Controllers
{
    [Route("[controller]")]
    public class TareaController : ControllerBase
    {
       
        private readonly ITareaService _tareaService;


        public TareaController(ITareaService tareaService)
        {
            
            _tareaService = tareaService;
        }

        [HttpGet]   
        [ProducesResponseType(typeof(List<TareaResponseDTOs>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ListarTareas()
        {

            var response = await _tareaService.Listar();

            return Ok(response);

        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TareaResponseDTOs),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] 
        [ProducesResponseType(typeof(ProblemDetails),StatusCodes.Status500InternalServerError)]       
        public async Task<IActionResult> ObtenerPorID(int id)
        {
            
            var response = await _tareaService.ObtenerPorID(id);

            if(response == null) return NotFound();

            return Ok(response);    
        }

        [HttpPost]
        [ProducesResponseType(typeof(TareaResponseDTOs), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Crear(TareaCreateDTOs  dto)
        {

            if(!ModelState.IsValid) return BadRequest(ModelState);

            var response = await _tareaService.Crear(dto);               


             return CreatedAtAction(nameof(ObtenerPorID), new {id= response.TareaID}, response);     

           
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(TareaResponseDTOs), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Actualizar(int id, TareaUpdateDTOs dto)
        {
            // 1. Buscar entidad existente
            var response = await _tareaService.Actualizar(id, dto);

            //Validar si exite
            if(response == null) return NotFound();

            return Ok(response);


        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails),StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Eliminar(int id)
        {
            var response = await _tareaService.Eliminar(id);

            if(!response) return NotFound();

            return NoContent();
        }



        [HttpGet("filtrar")]
        [ProducesResponseType(typeof(List<TareaResponseDTOs>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Filtrar(int? estadoID, int? PrioridadID)
        {
           var response = await _tareaService.Filtrar(estadoID, PrioridadID);

            return Ok(response);
        }


    }
}
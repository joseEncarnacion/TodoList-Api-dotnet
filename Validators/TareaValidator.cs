using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using todo.api.Data;
using todo.api.DTOs.Tarea;
using todo.api.Exceptions;

namespace todo.api.Validators
{
    public class TareaValidator
    {
        private readonly AppDbContext _context;
        public TareaValidator(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> UsuarioExiste(int usuarioID)
        {
            return await _context.Usuarios
            .AnyAsync(u => u.UsuarioID == usuarioID);
        }

        public async Task<bool> EstadoExiste(int estadoID)
        {
            return await _context.Estados
            .AnyAsync(e => e.EstadoID == estadoID);
        }

        public async Task<bool> PrioridadExiste(int prioridadID)
        {
            return await _context.Prioridades
            .AnyAsync(p => p.PrioridadID == prioridadID);
        }

        public async Task ValidarCrear(TareaCreateDTOs dto)
        {
            if(!await UsuarioExiste(dto.UsuarioID))
            {
                throw new BusinessException("El Usuario no existe");
            }

            if(! await EstadoExiste(dto.EstadoID))
            {
                throw new BusinessException("El Estado no existe");
            }

            if(! await PrioridadExiste(dto.PrioridadID))
            {
                throw new BusinessException("La Prioridad no existe");
            }
        }

        public async Task ValidarActualizar(TareaUpdateDTOs dto)
        {
             if(! await EstadoExiste(dto.EstadoID))
            {
                throw new BusinessException("El Estado no existe");
            }

            if(! await PrioridadExiste(dto.PrioridadID))
            {
                throw new BusinessException("La Prioridad no existe");
            }
        }
        

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using todo.api.DTOs.Tarea;

namespace todo.api.Services
{
    public interface ITareaService
    {
        
        public Task<List<TareaResponseDTOs>> Listar();
        public Task<TareaResponseDTOs?> ObtenerPorID(int id);
        public Task<TareaResponseDTOs> Crear(TareaCreateDTOs dto);

        public Task<List<TareaResponseDTOs>> Filtrar(int? estadoID, int? prioridadID);  

        public Task<TareaResponseDTOs?> Actualizar(int id, TareaUpdateDTOs dto);

        public Task<bool> Eliminar(int id);     
        
    }
}
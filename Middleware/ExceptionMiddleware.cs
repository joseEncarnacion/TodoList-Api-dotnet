using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using todo.api.Exceptions;

namespace todo.api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                
                await _next(context);
            }
            catch (BusinessException ex)
            {
                
                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                var problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Regla de negocio no cumplida",
                    Detail = ex.Message
                };

                await context.Response.WriteAsJsonAsync(problemDetails);
            }
            catch(Exception ex)
            {

                _logger.LogError(ex, "Error inesperado al procesar la solicitud");

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                var problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Error interno del servidor",
                    Detail = "Ocurrió un error inesperado al procesar su solicitud",
                };

                await context.Response.WriteAsJsonAsync(problemDetails);
            }
        }
    }
}
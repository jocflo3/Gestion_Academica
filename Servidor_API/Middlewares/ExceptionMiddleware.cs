using Servidor_API.Repositories.Interfaces;
using System.Net;
using System.Text.Json;
using Servidor_API.Excepciones;

namespace Servidor_API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (BusinessException ex)
            {
                context.Response.ContentType = "application/json";

                context.Response.StatusCode = ex.StatusCode;

                var response = new
                {
                    error = ex.ErrorCode,
                    message = ex.Message
                };

                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                await ManejarExcepcionAsync(context, ex);
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";

                context.Response.StatusCode = 500;

                var response = new
                {
                    error = "INTERNAL_ERROR",
                    message = "Error interno"
                };

                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                await ManejarExcepcionAsync(context, ex);
            }
        }

        private async Task ManejarExcepcionAsync(
            HttpContext context,
            Exception ex)
        {
            var endpoint = context.Request.Path;
            var metodo = context.Request.Method;
            var usuario = context.User?.Identity?.Name;
            var ip = context.Connection.RemoteIpAddress?.ToString();

            using var scope = context.RequestServices.CreateScope();

            var bitacoraRepo = scope.ServiceProvider.GetRequiredService<IBitacoraRepository>();

            await bitacoraRepo.RegistrarError(
                ex.Message,
                ex.StackTrace ?? "",
                endpoint,
                metodo,
                usuario,
                ip
            );
        }
    }
}
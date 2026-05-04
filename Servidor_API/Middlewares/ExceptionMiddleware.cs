using Servidor_API.Repositories.Interfaces;
using System.Net;
using System.Text.Json;

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
            catch (Exception ex)
            {
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

            context.Response.ContentType = "application/json";

            context.Response.StatusCode = 500;

            var response = new
            {
                mensaje = ex.Message
            };

            var json = System.Text.Json.JsonSerializer.Serialize(response);

            await context.Response.WriteAsync(json);
        }
    }
}
using DotNetOrchestra.Server.Exceptions;

namespace DotNetOrchestra.Server.Startup
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (BadRequestException ex)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync($"Неверный запрос: {ex.Message}");
            }
            catch (NotFoundException ex)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync($"Ресурс не найден: {ex.Message}");
            }
            catch (ConflictException ex)
            {
                context.Response.StatusCode = 409;
                await context.Response.WriteAsync($"Ошибка: {ex.Message}");
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync($"Необработанная ошибка: {ex.Message}");
            }
        }
    }
}

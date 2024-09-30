using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace MiddleWare;

public class GlobalExceptionHandlerMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (InvalidOperationException)
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsJsonAsync("Not found");
        }
        catch (Exception)
        {
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync("Unexpected error occured");
        }
    }
}
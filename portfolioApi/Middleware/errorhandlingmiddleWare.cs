using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace portfolioApi.Middleware;

public class errorhandlingmiddleWare
{
    private readonly RequestDelegate _next;

    public errorhandlingmiddleWare(RequestDelegate next) => _next = next;

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);          // run the rest of the pipeline
        }
        catch (Exception ex)
        {
            // log ex...
            context.Response.StatusCode = 500;
            await context.Response.WriteAsJsonAsync(new 
            { 
                message = "An unexpected error occurred.",
                details = ex.Message
            });
        }
    }
}

using System.Net;

namespace ProductsWeb.Middleware;

public class ProductsServiceMiddleware : IMiddleware
{
    private readonly ILogger<ProductsServiceMiddleware> _logger;

    public ProductsServiceMiddleware(ILogger<ProductsServiceMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while processing the request");

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsync("An error occurred while processing your request.");
        }
    }
}

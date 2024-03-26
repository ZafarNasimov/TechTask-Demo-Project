namespace ProductsWeb.Middleware;

public static class ProductsServiceExtensions
{
    public static IApplicationBuilder UseProductsServiceMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ProductsServiceMiddleware>();
    }
}

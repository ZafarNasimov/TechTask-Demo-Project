using ProductsAPI.Automapper;
using ProductsAPI.Data;
using ProductsAPI.Services;

namespace ProductsAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddDbContext<AppDbContext>();
        builder.Services.AddScoped<DatabaseService>();
        builder.Services.AddAutoMapper(typeof(MappingProfile));

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAllOrigins", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            });
        });

        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "ProductsAPI", Version = "v1" });
        });

        var app = builder.Build();

        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "ProductsAPI v1");
            options.RoutePrefix = "swagger";
        });

        app.UseCors("AllowAllOrigins");

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
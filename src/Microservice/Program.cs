using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Microservice;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddProblemDetails();

        builder.Configuration.AddEnvironmentVariables();
        builder.Services.Configure<StorageSettings>(builder.Configuration.GetSection("STORAGE"));

        var app = builder.Build();

        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseExceptionHandler();
        app.UseStatusCodePages();

        app.UseHttpsRedirection();

        app.RegisterConfigEndpoints();
        app.RegisterFileEndpoints();

        app.MapFallback(async Task (context) =>
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            await context.Response.WriteAsync("The endpoint you are looking for does not exist!");
        });

        app.Run();
    }
}
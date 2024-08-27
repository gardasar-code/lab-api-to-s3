using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Microservice;

public static class EndpointRouteBuilderExtensions
{
    private static readonly string[] HttpHeadMethods = ["HEAD"];
    public static void RegisterFileEndpoints(this IEndpointRouteBuilder app)
    {
        var fileEndpoints = app.MapGroup("/file");

        // GET file/1
        fileEndpoints.MapGet("{objectId}", (string objectId) => Results.Ok()).WithOpenApi();
        
        // HEAD file/1
        fileEndpoints.MapMethods("{objectId}", HttpHeadMethods, (string objectId) => Results.Ok()).WithOpenApi();
        
        // PUT file/1
        fileEndpoints.MapPut("{objectId}", (string objectId) => Results.Ok()).WithOpenApi();
        
        // DELETE file/1
        fileEndpoints.MapDelete("{objectId}", (string objectId) => Results.Ok()).WithOpenApi();
    }
}
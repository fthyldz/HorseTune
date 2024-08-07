using Presentation.API.Endpoints.V1;

namespace Presentation.API.Endpoints;

public static class EndpointsModule
{
    public static RouteGroupBuilder RegisterEndpoints(this RouteGroupBuilder group)
    {
        group.MapGroup("/v{version:apiVersion}")
            .RegisterV1Endpoints()
            .MapToApiVersion(1)
            .WithTags("V1 Api");
        
        return group;
    }
}
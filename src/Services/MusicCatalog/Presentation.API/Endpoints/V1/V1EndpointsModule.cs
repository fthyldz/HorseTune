namespace Presentation.API.Endpoints.V1;

public static class V1EndpointsModule
{
    public static RouteGroupBuilder RegisterV1Endpoints(this RouteGroupBuilder group)
    {
        group.MapGroup("/artists")
            .RegisterArtistsV1Endpoints()
            .WithTags("Artist Api");
        
        group.MapGroup("/genres")
            .RegisterGenresV1Endpoints()
            .WithTags("Genre Api");
        
        return group;
    }
}
namespace Smdb.Api.ActorMovies;

using Shared.Http;

public class ActorMoviesRouter : HttpRouter
{
    public ActorMoviesRouter(ActorMoviesController controller)
    {
        UseParametrizedRouteMatching();

        MapGet("/", controller.ReadActorMovies);
        MapPost("/", HttpUtils.ReadRequestBodyAsText, controller.CreateActorMovie);
        MapGet("/:id", controller.ReadActorMovie);
        MapPut("/:id", HttpUtils.ReadRequestBodyAsText, controller.UpdateActorMovie);
        MapDelete("/:id", controller.DeleteActorMovie);
    }
}
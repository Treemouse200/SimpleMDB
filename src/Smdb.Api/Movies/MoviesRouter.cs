namespace Smdb.Api.Movies;

using Shared.Http;

public class MoviesRouter : HttpRouter
{
    private readonly MoviesController controller;

    public MoviesRouter(MoviesController controller)
    {
        UseParametrizedRouteMatching();

        MapGet("/", controller.ReadMovies);
        MapPost("/", HttpUtils.ReadRequestBodyAsText, controller.CreateMovie);
        MapGet("/:id", controller.ReadMovie);
        MapPut("/:id", HttpUtils.ReadRequestBodyAsText, controller.UpdateMovie);
        MapDelete("/:id", controller.DeleteMovie);
        MapGet("/:id/actors", controller.GetMovieActors);
    }
}
namespace Smdb.Api;

using Smdb.Core.Db;
using Shared.Http;
using Smdb.Core.ActorMovies;
using Smdb.Core.Actors;
using Smdb.Core.Movies;
using Smdb.Core.Users;

using Smdb.Api.ActorMovies;
using Smdb.Api.Actors;
using Smdb.Api.Movies;
using Smdb.Api.Users;

public class App : HttpServer
{

    public override void Init()
    {
        var db = new MemoryDatabase();

        // Repositories
        var movieRepo = new MemoryMovieRepository(db);
        var actorRepo = new MemoryActorRepository(db);
        var userRepo = new MemoryUserRepository(db);
        var amRepo = new MemoryActorMovieRepository(db);

        // Services
        var movieServ = new DefaultMovieService(movieRepo);
        var actorServ = new DefaultActorService(actorRepo);
        var userServ = new DefaultUserService(userRepo);
        var amServ = new DefaultActorMovieService(amRepo);

        // Controllers
        var movieCtrl = new MoviesController(movieServ, amServ);
        var actorCtrl = new ActorsController(actorServ, amServ);
        var userCtrl = new UsersController(userServ);
        var amCtrl = new ActorMoviesController(amServ);

        // Routers
        var movieRouter = new MoviesRouter(movieCtrl);
        var actorRouter = new ActorsRouter(actorCtrl);
        var userRouter = new UsersRouter(userCtrl);
        var amRouter = new ActorMoviesRouter(amCtrl);
        var apiRouter = new HttpRouter();



        router.Use(HttpUtils.StructuredLogging);
        router.Use(HttpUtils.CentralizedErrorHandling);
        router.Use(HttpUtils.AddResponseCorsHeaders);
        router.Use(HttpUtils.DefaultResponse);
        router.Use(HttpUtils.ParseRequestUrl);
        router.Use(HttpUtils.ParseRequestQueryString);
        router.UseParametrizedRouteMatching();

        router.UseRouter("/api/v1", apiRouter);
        apiRouter.UseRouter("/movies", movieRouter);
        apiRouter.UseRouter("/actors", actorRouter);
        apiRouter.UseRouter("/users", userRouter);
        apiRouter.UseRouter("/actors-movies", amRouter);

    }

}

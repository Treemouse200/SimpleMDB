namespace Smdb.Core.ActorMovies;

using Shared.Http;
using Smdb.Core.Movies;
using Smdb.Core.Actors;

public class DefaultActorMovieService : IActorMovieService
{
    private IActorMovieRepository repo;

    public DefaultActorMovieService(IActorMovieRepository repo)
    {
        this.repo = repo;
    }

    public async Task<Result<PagedResult<ActorMovie>>> ReadActorMovies(int page, int size)
    {
        var result = await repo.ReadActorMovies(page, size);

        if (result == null)
            return new Result<PagedResult<ActorMovie>>(new Exception("No data"), 404);

        return new Result<PagedResult<ActorMovie>>(result);
    }

    public async Task<Result<ActorMovie>> CreateActorMovie(ActorMovie actorMovie)
    {
        var created = await repo.CreateActorMovie(actorMovie);

        if (created == null)
            return new Result<ActorMovie>(new Exception("Relationship already exists"), 409);

        return new Result<ActorMovie>(created, 201);
    }

    public async Task<Result<ActorMovie>> ReadActorMovie(int id)
    {
        var result = await repo.ReadActorMovie(id);

        if (result == null)
            return new Result<ActorMovie>(new Exception("Not found"), 404);

        return new Result<ActorMovie>(result);
    }

    public async Task<Result<ActorMovie>> UpdateActorMovie(int id, ActorMovie newData)
    {
        var updated = await repo.UpdateActorMovie(id, newData);

        if (updated == null)
            return new Result<ActorMovie>(new Exception("Not found"), 404);

        return new Result<ActorMovie>(updated);
    }

    public async Task<Result<ActorMovie>> DeleteActorMovie(int id)
    {
        var deleted = await repo.DeleteActorMovie(id);

        if (deleted == null)
            return new Result<ActorMovie>(new Exception("Not found"), 404);

        return new Result<ActorMovie>(deleted);
    }

    public async Task<Result<PagedResult<Movie>>> GetActorMovies(int actorId, int page, int size)
    {
        var result = await repo.GetActorMovies(actorId, page, size);

        if (result == null)
            return new Result<PagedResult<Movie>>(new Exception("No movies found"), 404);

        return new Result<PagedResult<Movie>>(result);
    }

    public async Task<Result<PagedResult<Actor>>> GetMovieActors(int movieId, int page, int size)
    {
        var result = await repo.GetMovieActors(movieId, page, size);

        if (result == null)
            return new Result<PagedResult<Actor>>(new Exception("No actors found"), 404);

        return new Result<PagedResult<Actor>>(result);
    }
}
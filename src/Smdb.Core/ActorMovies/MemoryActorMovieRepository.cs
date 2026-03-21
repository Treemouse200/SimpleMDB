namespace Smdb.Core.ActorMovies;

using Shared.Http;
using Smdb.Core.Db;
using Smdb.Core.Movies;
using Smdb.Core.Actors;

public class MemoryActorMovieRepository : IActorMovieRepository
{
    private MemoryDatabase db;

    public MemoryActorMovieRepository(MemoryDatabase db)
    {
        this.db = db;
    }

    public async Task<PagedResult<ActorMovie>?> ReadActorMovies(int page, int size)
    {
        int totalCount = db.ActorMovies.Count;
        int start = Math.Clamp((page - 1) * size, 0, totalCount);
        int length = Math.Clamp(size, 0, totalCount - start);

        var values = db.ActorMovies.Slice(start, length);
        return await Task.FromResult(new PagedResult<ActorMovie>(totalCount, values));
    }

    public async Task<ActorMovie?> CreateActorMovie(ActorMovie actorMovie)
    {


        if (db.ActorMovies.Any(am => am.ActorId == actorMovie.ActorId && am.MovieId == actorMovie.MovieId))
        {
            return null;
        }

        actorMovie.Id = db.NextActorMovieId();
        db.ActorMovies.Add(actorMovie);

        return await Task.FromResult(actorMovie);
    }

    public async Task<ActorMovie?> ReadActorMovie(int id)
    {
        return await Task.FromResult(
            db.ActorMovies.FirstOrDefault(am => am.Id == id));
    }

    public async Task<ActorMovie?> UpdateActorMovie(int id, ActorMovie newData)
    {
        var existing = db.ActorMovies.FirstOrDefault(am => am.Id == id);

        if (existing != null)
        {
            existing.ActorId = newData.ActorId;
            existing.MovieId = newData.MovieId;
        }

        return await Task.FromResult(existing);
    }

    public async Task<ActorMovie?> DeleteActorMovie(int id)
    {
        var existing = db.ActorMovies.FirstOrDefault(am => am.Id == id);

        if (existing != null)
        {
            db.ActorMovies.Remove(existing);
        }

        return await Task.FromResult(existing);
    }

    public async Task<PagedResult<Movie>?> GetActorMovies(int actorId, int page, int size)
    {
        var movieIds = db.ActorMovies.Where(am => am.ActorId == actorId).Select(am => am.MovieId).ToList();

        var movies = db.Movies.Where(m => movieIds.Contains(m.Id)).ToList();

        int totalCount = movies.Count;
        int start = Math.Clamp((page - 1) * size, 0, totalCount);
        int length = Math.Clamp(size, 0, totalCount - start);

        var values = movies.Slice(start, length);
        return await Task.FromResult(new PagedResult<Movie>(totalCount, values));
    }

    public async Task<PagedResult<Actor>?> GetMovieActors(int movieId, int page, int size)
    {
        var actorIds = db.ActorMovies
            .Where(am => am.MovieId == movieId)
            .Select(am => am.ActorId)
            .ToList();

        var actors = db.Actors
            .Where(a => actorIds.Contains(a.Id))
            .ToList();

        int totalCount = actors.Count;
        int start = Math.Clamp((page - 1) * size, 0, totalCount);
        int length = Math.Clamp(size, 0, totalCount - start);

        var values = actors.Slice(start, length);
        return await Task.FromResult(new PagedResult<Actor>(totalCount, values));
    }
}
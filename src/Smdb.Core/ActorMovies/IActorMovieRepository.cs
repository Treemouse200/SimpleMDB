namespace Smdb.Core.ActorMovies;

using Smdb.Core.Db;
using Smdb.Core.Movies;
using Smdb.Core.Actors;
using Shared.Http;

public interface IActorMovieRepository
{
    Task<PagedResult<ActorMovie>?> ReadActorMovies(int page, int size);
    Task<ActorMovie?> CreateActorMovie(ActorMovie actorMovie);
    Task<ActorMovie?> ReadActorMovie(int id);
    Task<ActorMovie?> UpdateActorMovie(int id, ActorMovie newData);
    Task<ActorMovie?> DeleteActorMovie(int id);

    Task<PagedResult<Movie>?> GetActorMovies(int actorId, int page, int size);
    Task<PagedResult<Actor>?> GetMovieActors(int movieId, int page, int size);
}
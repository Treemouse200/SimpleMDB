namespace Smdb.Core.Users;

using Shared.Http;
public interface IUserRepository
{
    Task<PagedResult<User>?> ReadUsers(int page, int size);
    Task<User?> CreateUser(User newUser);
    Task<User?> ReadUser(int id);
    Task<User?> UpdateUser(int id, User newUserData);
    Task<User?> DeleteUser(int id);
}

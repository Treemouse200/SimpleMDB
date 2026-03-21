namespace Smdb.Core.Users;

public class User
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }

    public User(int id, string username, string email)
    {
        Id = id;
        UserName = username;
        Email = email;


    }
    public override string ToString()
    {
        return $"User[Id={Id}, Username={UserName}, E-mail={Email}]";
    }
}

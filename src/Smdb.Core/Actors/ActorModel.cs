namespace Smdb.Core.Actors;

public class Actor
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int BirthYear { get; set; }
    public string Description { get; set; }

    public Actor(int id, string name, int birthYear, string description)
    {
        Id = id;
        Name = name;
        BirthYear = birthYear;
        Description = description;

    }
    public override string ToString()
    {
        return $"Actor[Id={Id}, Name={Name}, Birth Year={BirthYear}, Description={Description}]";
    }
}

using LectionServer.Contracts;
using LectionServer.Models;

namespace LectionServer.Services;

public class UserService
{
    private string name = "User.json";
    private JSONManager<List<User>> jsonManager;

    public UserService() 
    {
        jsonManager = new(name);
    }
    private readonly List<User> _users = new();

    public User? GetUser(string email, string password)
    {
        _users.Clear();
        _users.AddRange(jsonManager.Get() == null ? new() : jsonManager.Get());

        return _users.SingleOrDefault(x => x.Email == email && x.Password == password);
    }


    public User AddUser(UserRequest request)
    {
        var book = new User
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Email = request.Email,
            Password = request.Password
        };
        _users.Add(book);
        jsonManager.Set(_users);

        return book;
    }
}
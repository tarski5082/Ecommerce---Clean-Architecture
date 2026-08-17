using Infrastructure.Models;
namespace Infrastructure.Repositories.Abstractions;

public interface IUserRepository
{
    User? GetUserByUsername(string username);
    void AddUser(User user);
    IEnumerable<User> GetAllUsers();
    public bool UpdateUser(User user);
    User? GetUserById(Guid Id);
}
using Core.Models;

namespace Core.UseCases.Abstractions;

public interface IUserUseCases
{
    User AuthenticateAndGetUser(AuthenticationRequest request);
    void Register(RegisterRequest request);
    IEnumerable<User> GetAllUsers();

    void addBillingAdress(string username,Address address);
}
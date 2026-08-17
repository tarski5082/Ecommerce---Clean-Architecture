using Core.Models;
using Core.Models.Request;
namespace Core.UseCases.Abstractions;

public interface IUserUseCases
{
    User AuthenticateAndGetUser(AuthenticationRequest request);
    void Register(RegisterRequest request);
    IEnumerable<User> GetAllUsers();
    ProfileRequest GetProfile(Guid userId);
    public void UpdateLivraison(AddressRequest request,Guid userId);
    public void UpdateFacturation(AddressRequest request,Guid userId);

}
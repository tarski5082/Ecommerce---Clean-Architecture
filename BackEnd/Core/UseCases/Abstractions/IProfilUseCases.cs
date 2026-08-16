using Core.Models.Request;
namespace Core.UseCases.Abstractions;

public interface IProfilUseCases
{
    void UpdateProfile(ProfileRequest request);
}

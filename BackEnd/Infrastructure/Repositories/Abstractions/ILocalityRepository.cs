namespace Infrastructure.Repositories.Abstractions;
using Infrastructure.Models;

public interface ILocalityRepository
{
    int GetLocalityId(Locality locality);
    int AddLocality(Locality locality);
    Locality? GetLocality(int id);
    bool LocalityExist(Locality locality);
    bool UpdateLocality(Locality locality);
    bool DeleteLocality(int id);
}
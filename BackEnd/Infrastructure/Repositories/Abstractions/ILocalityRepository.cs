namespace Infrastructure.Repositories.Abstractions;
using Infrastructure.Models;

public interface ILocalityRepository
{
    int addLocality(Locality locality);

    Locality? GetLocalityById(int id);
    bool LocalityExist(Locality locality);
    bool UpdateLocality(Locality locality);
    bool DeleteLocality(int id);
}
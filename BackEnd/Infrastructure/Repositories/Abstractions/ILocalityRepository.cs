namespace Infrastructure.Repositories.Abstractions;
using Infrastructure.Models;

public interface ILocalityRepository
{
    
    int AddLocality(Locality locality);
    int? GetLocalityId(Locality locality);
    Locality? GetLocalityById(int id);
    bool UpdateLocality(Locality locality);
    
}
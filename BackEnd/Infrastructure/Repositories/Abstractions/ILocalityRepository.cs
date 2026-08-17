namespace Infrastructure.Repositories.Abstractions;
using Infrastructure.Models;
using Core.Models.Request;
public interface ILocalityRepository
{
    
    int AddLocality(Locality locality);
    int? GetLocalityId(Locality locality);
    Locality? GetLocalityById(int id);
    bool UpdateLocality(Locality locality);
    int? GetLocalityId(LocalityRequest request);
    
}
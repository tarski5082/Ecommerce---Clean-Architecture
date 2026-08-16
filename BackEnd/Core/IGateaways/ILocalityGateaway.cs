using Core.Models;
namespace Core.IGateways;

public interface ILocalityGateaway
{
    int? AddLocality(Locality locality);
    int? GetLocalityId(Locality locality);
    Locality? GetLocalityById(int id);
    bool UpdateLocality(Locality locality);
}
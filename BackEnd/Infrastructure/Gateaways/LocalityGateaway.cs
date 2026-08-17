namespace Infrastructure.Gateways;
using Infrastructure.Repositories.Abstractions;
using Infrastructure.Models;
using Core.IGateways;
using Core.Models.Request;

public class LocalityGateaway:ILocalityGateaway
{
    private readonly ILocalityRepository _localityRepository;

    public LocalityGateaway(ILocalityRepository localityRepository)
    {
        _localityRepository=localityRepository;
    }

    public int AddLocality(Core.Models.Locality localite)
    {
        return _localityRepository.AddLocality(new Locality
        {
            CodePostal=localite.CodePostal,
            Ville=localite.Ville,
            Province=localite.Province
        });
    }

    public Core.Models.Locality GetLocalityById (int id)
    {
        var localite = _localityRepository.GetLocalityById(id);
        return new Core.Models.Locality
        {
            CodePostal=localite.CodePostal,
            Ville=localite.Ville,
            Province=localite.Province
        };
    }
    public int? GetLocalityId(Core.Models.Locality localite)
    {
        return _localityRepository.GetLocalityId(new Locality
        {
            CodePostal=localite.CodePostal,
            Ville=localite.Ville,
            Province=localite.Province
        });
    }

    public bool UpdateLocality(Core.Models.Locality localite)
    {
        return _localityRepository.UpdateLocality(new Locality
        {
            CodePostal=localite.CodePostal,
            Ville=localite.Ville,
            Province=localite.Province
        });
    }

    public int? GetLocalityId(LocalityRequest request)
    {
        return _localityRepository.GetLocalityId(request);
    }
}
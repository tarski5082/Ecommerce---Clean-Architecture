namespace Infrastructure.Gateways;
using Infrastructure.Repositories;
using Infrastructure.Models;
public class LocalityGateaway
{
    private readonly LocalityRepository _localityRepository;

    public LocalityGateaway(LocalityRepository localityRepository)
    {
        _localityRepository=localityRepository;
    }

    public int AddLocality(Locality localite)
    {
        return _localityRepository.AddLocality(localite);
    }
}
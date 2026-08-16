using Core.Models;
using Core.UseCases.Abstractions;
using Core.IGateways;
using Core.Models.Request;
using System.Security.Cryptography.X509Certificates;

namespace Core.UseCases;

public class ProfileUseCases:IProfilUseCases
{
    private readonly IUserGateway _userGateaway;
    private readonly IAddressGateaway _addressGateaway;
    private readonly ILocalityGateaway _localityGateaway;

    public ProfileUseCases(IUserGateway userGateaway,IAddressGateaway addressGateaway,ILocalityGateaway localityGateaway)
    {
        _userGateaway=userGateaway;
        _addressGateaway=addressGateaway;
        _localityGateaway=localityGateaway;

    }

    public void UpdateProfile(ProfileRequest request)
    {
        
    }
}
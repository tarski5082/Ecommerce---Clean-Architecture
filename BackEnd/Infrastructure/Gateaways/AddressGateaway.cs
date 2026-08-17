namespace Infrastructure.Gateways;
using Infrastructure.Repositories;
using Infrastructure.Models;
using Core.IGateways;
using Infrastructure.Repositories.Abstractions;
public class AddressGateaway:IAddressGateaway
{
    private readonly IAddressRepository _addressRepository;

    public AddressGateaway(IAddressRepository addressRepository)
    {
        _addressRepository=addressRepository;
    }
     public bool UpdateAddress(Core.Models.Address address)
    {
       var _address = Address.ToInfraModel(address);
       return _addressRepository.UpdateAddress(_address);
    }
    public Core.Models.Address? GetAddressById(int id)
    {
        var address = _addressRepository.GetAddressById(id);
        if(address is null) return null;
        return new Core.Models.Address
        {
            Id=id,
            Rue = address.Rue,
            Numero = address.Numero,
            Boite = address.Boite,
            IdLocalite=address.IdLocalite
        };
    }
   

    public int AddAdress(Core.Models.Address address)
    {
    
        var _address = Address.ToInfraModel(address);
        return _addressRepository.AddAddress(_address);
    }
    public Core.Models.Address? GetAddress(int id)
    {
        var _address = _addressRepository.GetAddressById(id);
        if (_address is null) return null;
        return new Core.Models.Address
        {
            Id = _address.Id,
            Rue = _address.Rue,
            Numero = _address.Numero,
            Boite = _address.Boite,
            IdLocalite=_address.IdLocalite
        };
    }

    public int? GetAddressId(Core.Models.Address address)
    {
        var _address = Address.ToInfraModel(address);
        return _addressRepository.GetAddressId(_address);
    }
    
}
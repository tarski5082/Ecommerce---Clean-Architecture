namespace Infrastructure.Gateways;
using Infrastructure.Repositories;
using Infrastructure.Models;
using Core.IGateways;
public class AddressGateaway:IAddressGateaway
{
    private readonly AddressRepository _addressRepository;
    private readonly LocalityRepository localityRepository;

    public AddressGateaway(AddressRepository addressRepository)
    {
        _addressRepository=addressRepository;
    }
     public void UpdateAddress(Core.Models.Address address)
    {
       var _address = new Address
       {
           Id=address.Id,
           Rue=address.Rue,
           Numero=address.Numero,
           Boite=address.Boite,
           IdLocalite=address.Localite.Id
       };
       _addressRepository.UpdateAddress(_address);
    }

   

    public int AddAdress(Core.Models.Address address)
    {
    
        var _address = new Address
       {
           Id=address.Id,
           Rue=address.Rue,
           Numero=address.Numero,
           Boite=address.Boite,
           IdLocalite=address.Localite.Id
       };
        return _addressRepository.AddAdress(_address);
    }
    public Core.Models.Address? GetAddress(int id)
    {
        var _address = _addressRepository.GetAddress(id);
        
        return new Core.Models.Address
        {
            Id = _address.Id,
            Rue = _address.Rue,
            Numero = _address.Numero,
            Boite = _address.Boite
        };
    }
    
}
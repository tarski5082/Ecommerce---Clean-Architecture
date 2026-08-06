using System.ComponentModel;
using System.Reflection.Metadata.Ecma335;
using Core.IGateways;
using Infrastructure.Models;
using Infrastructure.Repositories.Abstractions;

namespace Infrastructure.Gateways;

public  class UserGateaway : IUserGateway
{
    private readonly IUserRepository _userRepository;
    private readonly IAddressRepository _addressRepository;
    public UserGateaway(IUserRepository userRepository,IAddressRepository addressRepository)
    {
        _userRepository = userRepository;
        _addressRepository=addressRepository;
    }

    public Address convertToAddressInfrastructure(Core.Models.Address address)
    {
        return new Address
        {
            Id = address.Id,
            Rue = address.Rue,
            Numero=address.Numero,
            Boite=address.Boite,
            IdLocalite=address.IdLocalite
        };
    }

    public void AddUser(String username,string passwordHash)
    {
        var user = new User
        {
            Username = username,
            PasswordHash=passwordHash,
            IsAdmin = false
        };
        _userRepository.AddUser(user);
    }

    public IEnumerable<Core.Models.User> GetAllUsers()
    {
        var users = _userRepository.GetAllUsers();
        return users.Select(user => new Core.Models.User
        {
            Id = user.Id,
            Username = user.Username,
            IsAdmin = user.IsAdmin,
            Nom=user.Nom,
            Prenom=user.Prenom
        });
    }

    public string? GetUserPasswordHash(string username) 
        {
            var user = _userRepository.GetUserByUsername(username);
            return user?.PasswordHash;
        }

    public Core.Models.User? GetUserByUsername(string username)
    {
        var infraUser = _userRepository.GetUserByUsername(username);
        if (infraUser == null) return null;
        return new Core.Models.User
        {
            Id = infraUser.Id,
            Username = infraUser.Username,
            IsAdmin = infraUser.IsAdmin,
            Nom = infraUser.Nom,
            Prenom=infraUser.Prenom
        };
    }

    public void UpdateBillingAddress(string username,Core.Models.Address address)
    {
        var user = _userRepository.GetUserByUsername(username);
        var _address = _addressRepository.addBillingAdress(convertToAddressInfrastructure(address));
        user.IdFacturation = _address;
        _userRepository.UpdateUser(user);

    }

    public void UpdateDeliveryAddress(string username,Core.Models.Address address)
    {
        var user = _userRepository.GetUserByUsername(username);
        var _address = _addressRepository.addBillingAdress(convertToAddressInfrastructure(address));
        user.IdLivraison = _address;
        _userRepository.UpdateUser(user);

    }

    public void addBillingAdress(string username, Core.Models.Address address)
    {
        var _address = convertToAddressInfrastructure(address);
        _addressRepository.addBillingAdress(_address);
    }

    
}
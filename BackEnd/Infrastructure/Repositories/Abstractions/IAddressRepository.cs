namespace Infrastructure.Repositories.Abstractions;
using Infrastructure.Models;
public interface IAddressRepository
{
    int GetAddressId(Address address);
    int AddAdress(Address adress); 
    Address? GetAddress(int id);
    bool UpdateAddress(Address address);
    bool DeleteAddress(int id);
    bool AddressExist(Address adress);

}
namespace Infrastructure.Repositories.Abstractions;
using Infrastructure.Models;
public interface IAddressRepository
{
    Address? GetAddressById(int id);
    int? GetAddressId(Address address);
    int AddAddress(Address address); 
    bool UpdateAddress(Address address);

}
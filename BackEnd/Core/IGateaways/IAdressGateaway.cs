using Core.Models;
namespace Core.IGateways;

public interface IAddressGateaway
{

    Address? GetAddressById(int id);
    int? GetAddressId(Address address);
    int AddAdress(Address address); 
    bool UpdateAddress(Address address);

}
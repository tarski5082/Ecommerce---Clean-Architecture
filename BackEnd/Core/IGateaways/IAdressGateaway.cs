using Core.Models;
namespace Core.IGateways;

public interface IAddressGateaway
{

    Address? GetAddress(int id);
    int AddAdress(Address address);

}
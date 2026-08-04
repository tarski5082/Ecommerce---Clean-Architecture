namespace Infrastructure.Repositories.Abstractions;
using Infrastructure.Models;
public interface IAddressRepository
{
    int addBillingAdress(Address adress);
    int addDeliveryAdress(Address adress);

    Address? GetBillingAddress(int id);
    Address? GetDeliveryAddress(int id);

    bool UpdateBillingAddress(Address address);
    bool UpdateDeliveryAddress(Address address);

    bool DeleteBillingAddress(int id);
    bool DeleteDeliveryAddress(int id);

}
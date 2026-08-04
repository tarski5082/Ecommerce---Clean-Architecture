namespace Infrastructure.Repositories.Abstractions;
using Infrastructure.Models;
public interface IAddressRepository
{
    int addBillingAdress(Address adress);
    int addDeliveryAdress(Address adress);

    Address? GetBillingAddress(int id);
    Address? GetDeliveryAddress(int id);

    bool UpdateBillingAddress(int id);
    bool UpdateDeliveryAddress(int id);

    void DeleteBillingAddress(int id);
    void DeleteDeliveryAddress(int id);

}
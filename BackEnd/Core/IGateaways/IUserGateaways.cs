using System;
using Core.Models;

namespace Core.IGateways;

public interface IUserGateway
{
    string? GetUserPasswordHash(string username);
    User? GetUserByUsername(string username);
    void AddUser(string username, string passwordHash);
    IEnumerable<User> GetAllUsers();
    void UpdateBillingAddress(string username,Address address);

    void addBillingAdress(string username,Address address);
}
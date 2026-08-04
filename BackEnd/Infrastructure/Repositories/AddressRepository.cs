namespace Infrastructure.Repositories;

using Infrastructure.Repositories.Abstractions;
using Infrastructure.Models;
using Dapper;
using MySql.Data.MySqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;

public class AddressRepository(IConfiguration configuration) : IAddressRepository
{

    private readonly string _connectionString = configuration.GetConnectionString("DefaultConnection")
                            ?? throw new ArgumentNullException(nameof(configuration), "Database connection string 'DefaultConnection' not found.");

    private IDbConnection CreateConnection() => new MySqlConnection(_connectionString);
    public int addBillingAdress(Address adress)
    {
        const string sql = @"
            INSERT INTO AdresseFacturation (Rue, Numero, Boite, id_localite)
            VALUES (@Rue, @Numero, @Boite, @IdLocalite);

            SELECT LAST_INSERT_ID();
            ";

            using(var connection = CreateConnection())
        {
            connection.Open();
            return connection.QuerySingle<int>(sql,adress);
        }
    }
    public int addDeliveryAdress(Address adress)
    {
         const string sql = @"
            INSERT INTO AdresseLivraison(Rue, Numero, Boite, id_localite)
            VALUES (@Rue, @Numero, @Boite, @IdLocalite);

            SELECT LAST_INSERT_ID();
            ";

            using(var connection = CreateConnection())
        {
            connection.Open();
            return connection.QuerySingle<int>(sql,adress);
        }
    }

    public Address? GetBillingAddress(int id)
    {
        using(var connection = CreateConnection())
        {
            connection.Open();
            return connection.QuerySingleOrDefault<Address>("SELECT * FROM AdresseFacturation WHERE id=@id",new {id=id});
        }
    }
    public Address? GetDeliveryAddress(int id)
    {
        using(var connection = CreateConnection())
        {
            connection.Open();
            return connection.QuerySingleOrDefault<Address>("SELECT * FROM AdresseLivraison WHERE id=@id",new {id=id});
        }
    }

    public bool UpdateBillingAddress(Address address)
    {
        const string sql = @"
            UPDATE AdresseFacturation
            SET Rue = @Rue,
                Numero = @Numero,
                Boite = @Boite,
                id_localite = @IdLocalite
            WHERE Id = @Id;
            ";
        using (var connection = CreateConnection())
        {
            connection.Open();
            int affectedRaw = connection.Execute(sql,address);
            return affectedRaw>0;
        }
    }
    public bool UpdateDeliveryAddress(Address address)
    {
        const string sql = @"
            UPDATE AdresseLivraison
            SET Rue = @Rue,
                Numero = @Numero,
                Boite = @Boite,
                id_localite = @IdLocalite
            WHERE Id = @Id;
            ";
        using (var connection = CreateConnection())
        {
            connection.Open();
            int affectedRaw = connection.Execute(sql,address);
            return affectedRaw>0;
        }
    }

    public bool DeleteBillingAddress(int id)
    {
        using(var connection = CreateConnection())
        {
            connection.Open();
            int affectedRaw = connection.Execute(
                "DELETE FROM Stock WHERE id = @id", new {id = id}
            );
            return affectedRaw>0;
        }
    }
    public bool DeleteDeliveryAddress(int id)
    {
        using(var connection = CreateConnection())
        {
            connection.Open();
            int affectedRaw = connection.Execute(
                "DELETE FROM Stock WHERE id = @id", new {id = id}
            );
            return affectedRaw>0;
        }
    }

}
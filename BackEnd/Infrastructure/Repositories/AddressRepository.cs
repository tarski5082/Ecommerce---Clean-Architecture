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
    public int AddAdress(Address adress)
    {
        const string sql = @"
            INSERT INTO Adresse (Rue, Numero, Boite)
            VALUES (@Rue, @Numero, @Boite);

            SELECT LAST_INSERT_ID();
            ";

            using(var connection = CreateConnection())
        {
            connection.Open();
            return connection.QuerySingle<int>(sql,adress);
        }
    }
    

    public Address? GetAddressById(int id)
    {
        using(var connection = CreateConnection())
        {
            connection.Open();
            return connection.QuerySingleOrDefault<Address>("SELECT * FROM Adresse WHERE Id=@Id",new {Id=id});
        }
    }

    public bool UpdateAddress(Address address)
    {
        const string sql = @"
            UPDATE Adresse
            SET Rue = @Rue,
                Numero = @Numero,
                Boite = @Boite
            WHERE Id = @Id;
            ";
        using (var connection = CreateConnection())
        {
            connection.Open();
            int affectedRaw = connection.Execute(sql,address);
            return affectedRaw>0;
        }
    }
    

    
    public int? GetAddressId(Address address)
    {
        const string sql = @"SELECT Id FROM Adresse
                            WHERE Rue=@Rue
                            AND Numero=@Numero
                            AND (
                                Boite = @Boite
                                OR (Boite IS NULL AND @Boite IS NULL)
                            )
                            AND IdLocalite=@IdLocalite;";
        using(var connection = CreateConnection())
        {
            connection.Open();
            return connection.QueryFirstOrDefault<int?>(sql,address);
        }
    }

    
   

}
namespace Infrastructure.Repositories;
using Infrastructure.Models;
using Infrastructure.Repositories.Abstractions;
using Dapper;
using MySql.Data.MySqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;


public class LocalityRepository(IConfiguration configuration):ILocalityRepository
{

    private readonly string _connectionString = configuration.GetConnectionString("DefaultConnection")
                            ?? throw new ArgumentNullException(nameof(configuration), "Database connection string 'DefaultConnection' not found.");
    
    public IDbConnection CreateConnections()=>new MySqlConnection(_connectionString);
    public int AddLocality(Locality locality)
    {
        const string sql = @"INSERT INTO Localite (CodePostal,Ville,Province) 
        VALUES (@CodePostal,@Ville,@Province);
        SELECT LAST_INSERT_ID();
        ";
        using (var connection = CreateConnections()){
            connection.Open();
            return connection.QuerySingle<int>(sql,locality);
        }
    }

    public Locality GetLocalityById(int id)
    {
        using (var connection = CreateConnections())
        {
            connection.Open();
            connection.QuerySingleOrDefault<Locality>("SELECT * FROM Localite WHERE Id=@Id;",new {Id=id});
        }

        return new Locality();
    }
    public bool UpdateLocality(Locality locality)
    {
        return true;
    }

    public bool DeleteLocality(int id)
    {
        return true;
    }
}
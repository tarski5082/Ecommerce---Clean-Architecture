namespace Infrastructure.Repositories;
using Infrastructure.Models;
using Infrastructure.Repositories.Abstractions;
using Dapper;
using MySql.Data.MySqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;
using Core.Models.Request;

public class LocalityRepository(IConfiguration configuration):ILocalityRepository
{

    private readonly string _connectionString = configuration.GetConnectionString("DefaultConnection")
                            ?? throw new ArgumentNullException(nameof(configuration), "Database connection string 'DefaultConnection' not found.");
    
    public IDbConnection CreateConnections()=>new MySqlConnection(_connectionString);
    public int AddLocality(Locality locality)
{
        const string exist = @"SELECT Id FROM Localite WHERE
                            CodePostal = @CodePostal AND
                            Ville = @Ville AND
                            Province = @Province;";

        const string sql = @"INSERT INTO Localite (CodePostal, Ville, Province) 
                        VALUES (@CodePostal, @Ville, @Province);
                        SELECT LAST_INSERT_ID();";

        using (var connection = CreateConnections())
        {
            connection.Open();
            var existingId = connection.QuerySingleOrDefault<int?>(exist, locality);
            if (existingId.HasValue && existingId.Value > 0)
            {
                return existingId.Value;
            }
            return connection.QuerySingle<int>(sql, locality);
    }
}

    public int? GetLocalityId(Locality locality)
    {
        const string sql = @"SELECT Id FROM Localite WHERE 
                            CodePostal=@CodePostal AND
                            Ville=@Ville AND
                            Province=@Province;";
        using (var connection = CreateConnections())
        {
            connection.Open();
            return connection.QuerySingleOrDefault<int>(sql, new
            {
                CodePostal = locality.CodePostal,
                Ville = locality.Ville,
                Province = locality.Province
            });       
        }
    }

    public Locality? GetLocalityById(int id)
    {
        using (var connection = CreateConnections())
        {
            connection.Open();
            return connection.QuerySingleOrDefault<Locality>("SELECT * FROM Localite WHERE Id=@Id;",new {Id=id});
        }

        
    }
    public bool UpdateLocality(Locality locality)
    {
        const string sql = @"UPDATE Localite SET
                            CodePostal=@CodePostal,
                            Ville=@Ville,
                            Province=@Province
                            WHERE Id=@Id";
        using(var connection = CreateConnections())
        {
            connection.Open();
            int affectedRaw = connection.Execute(sql,locality);
            return affectedRaw>0;
        }
    }

    public int? GetLocalityId(LocalityRequest request)
    {
        const string sql =@"SELECT Id FROM Localite WHERE
                            CodePostal=@CodePostal,
                            Ville=@Ville,
                            Province=@Province;";
        using (var connection = CreateConnections())
        {
            connection.Open();
            return connection.QueryFirstOrDefault<int>(sql,request);
        }
    }

    
}
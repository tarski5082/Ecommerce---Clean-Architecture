using System.Data;
using Dapper;
using Infrastructure.Models;
using Infrastructure.Repositories.Abstractions;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace Infrastructure.Repositories;

public class CategoryRepository(IConfiguration configuration):ICategoryRepository
{
    private readonly string _connectionString = configuration.GetConnectionString("DefaultConnection")
                            ?? throw new ArgumentNullException(nameof(configuration), "Database connection string 'DefaultConnection' not found.");
    public IDbConnection CreateConnection()=>new MySqlConnection(_connectionString);
    public int AddCategory(Category category)
    {
        const string sql = @"INSERT INTO Categorie(Nom) VALUES Nom=@Nom;
                            SELECT LAST_INSERT_ID();";
        using (var connection = CreateConnection()){
            connection.Open();
            return connection.QuerySingle<int>(sql,category);
        }
    }

    public Category? GetCategory(int id)
    {
        const string sql =@"SELECT * FROM Categorie
                            WHERE Id = @Id;";
        using(var connection = CreateConnection())
        {
            connection.Open();
            return connection.QuerySingleOrDefault<Category>(sql,new{Id =id});
        }
    }

    public int? GetCategoryId(Category category)
    {
        const string sql = @"SELECT Id FROM Categorie
                            WHERE Nom = @Nom;";
        using(var connection = CreateConnection())
        {
            connection.Open();
            return connection.QuerySingleOrDefault<int>(sql,new {Nom = category.Nom});
        }
    }

    public void DeleteCategory(int id)
    {
        const string sql = @"DELETE FROM Categorie
                            WHERE Id=@Id;";
        using(var connection = CreateConnection())
        {
            connection.Open();
            connection.Execute(sql,new{Id=id});
        }
    }

    public bool UpdateCategory(Category category)
    {
        const string sql = @"UPDATE Categorie SET
                            Nom = @Nom
                            WHERE Id=@Id;";
        using (var connection = CreateConnection())
        {
            connection.Open();
            int affectedRaw = connection.Execute(sql,new{Id=category.Id});
            return affectedRaw>0;
        }
    }
}
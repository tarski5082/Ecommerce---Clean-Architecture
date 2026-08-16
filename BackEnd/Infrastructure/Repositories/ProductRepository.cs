namespace Infrastructure.Repositories;
using Infrastructure.Repositories.Abstractions;
using Infrastructure.Models;
using Microsoft.Extensions.Configuration;
using System.Data;
using Dapper;
using MySql.Data.MySqlClient;
public class ProductRepository(IConfiguration configuration):IProductRepository
{

    private readonly string _connectionString = configuration.GetConnectionString("DefaultConnection")
                            ?? throw new ArgumentNullException(nameof(configuration), "Database connection string 'DefaultConnection' not found.");

    private IDbConnection CreateConnection() => new MySqlConnection(_connectionString);
    public IEnumerable<Product> GetAllProducts()
    {
        const string sql = @"SELECT Id,Nom,Inventaire,PrixUnitaire,ImageUrl,IdCategorie FROM Produit;";
        using(var connection = CreateConnection())
        {
            connection.Open();
            return connection.Query<Product>(sql).ToList();
        }
    }
    public Product? GetProductById(int id)
    {
        const string sql = @"
        SELECT Id, Nom, Inventaire, PrixUnitaire, ImageUrl, IdCategorie
        FROM Produit
        WHERE Id = @Id;";

        using(var connection = CreateConnection()){
            connection.Open();
            return connection.QueryFirstOrDefault<Product>(sql, new { Id = id });
        }
    }
    public void AddProduct(Product product)
    {
        const string sql = @"
        INSERT INTO Produit
            (Nom, Inventaire, PrixUnitaire, ImageUrl, IdCategorie)
        VALUES
            (@Nom, @Inventaire, @PrixUnitaire, @ImageUrl, @IdCategorie);";

        using var connection = CreateConnection();
        connection.Execute(sql,product);
    }
    public void UpdateProduct(Product product)
    {
        const string sql = @"
        UPDATE Produit
        SET
            Nom = @Nom,
            Inventaire = @Inventaire,
            PrixUnitaire = @PrixUnitaire,
            ImageUrl = @ImageUrl,
            IdCategorie = @IdCategorie
        WHERE Id = @Id;";

        using var connection = CreateConnection();

            connection.Execute(sql, product);
    }
    public void DeleteProduct(int id)
    {
        const string sql = @"DELETE FROM Produit
                            WHERE Id=@Id";
        using(var connection = CreateConnection())
        {
            connection.Open();
            connection.Execute(sql,new{Id=id});
        }
    }

}
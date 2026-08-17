using System.Data;
using Dapper;
using Infrastructure.Models;
using Infrastructure.Repositories.Abstractions;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace Infrastructure.Repositories;

public class CartRepository(IConfiguration configuration) : ICartRepository
{
    private readonly string _connectionString = configuration.GetConnectionString("DefaultConnection")
                            ?? throw new ArgumentNullException(nameof(configuration), "Database connection string 'DefaultConnection' not found.");
    public IDbConnection CreateConnection()=>new MySqlConnection(_connectionString);

    public IEnumerable<Cart> GetAllCarts(Guid UserId)
    {
        const string sql = @"SELECT Id,UserId,Etat,Livraison FROM Panier;";
        using(var connection = CreateConnection())
        {
            connection.Open();
            return connection.Query<Cart>(sql).ToList();
        }
    }
    public Cart? GetCart(Guid Id)
    {
        const string sql =@"SELECT * FROM Panier
                            WHERE Id = @Id;";
        using(var connection = CreateConnection())
        {
            connection.Open();
            return connection.QuerySingleOrDefault<Cart>(sql,new{Id =Id});
        }
    }

    public Guid GetId(Guid cartId)
    {
        const string sql =@"SELECT Id FROM Panier
                            WHERE PanierId = @PanierId;";
        using(var connection = CreateConnection())
        {
            connection.Open();
            return connection.QueryFirstOrDefault<Guid>(sql,new{PanierId=cartId});
        }
    }
    public void CreateCart(Guid Id)
    {
        const string sql =@"INSERT INTO Panier(UserId) VALUES
                            (@UserId);";
        using(var connection = CreateConnection())
        {
            connection.Open();
            connection.Execute(sql,new{UserId=Id});
        }
    }
    public void Delete(Guid Id)
    {
        const string sql = @"DELETE FROM Panier
                            WHERE Id=@Id;";
        using(var connection = CreateConnection())
        {
            connection.Open();
            connection.Execute(sql,new{Id=Id});
        }
    }
    public void UpdateStatus(Cart cart)
    {
        const string sql = @"UPDATE Panier SET
                            Etat = @Etat
                            WHERE Id=@Id;";
        using (var connection = CreateConnection())
        {
            connection.Open();
            connection.Execute(sql,new{Id=cart.Id});
            
        }
    }

    public void AddCartItem(Guid cartId, IEnumerable<CartItem> items)
    {
        using var connection = CreateConnection();

            connection.Open();

            var transaction = connection.BeginTransaction();

            try
            {
                foreach (var item in items)
                {
                    var cartItem = new CartItem
                    {
                        Id = item.Id,
                        ProduitId = item.ProduitId,
                        Quantite = item.Quantite,
                        PanierId = cartId
                    };
                    var sql = "INSERT INTO Article (Id, ProduitId, Quantite,PanierId) VALUES (@Id, @ProduitId,@Quantite,@PanierId)";
                    connection.Execute(sql, cartItem, transaction);
                }

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
    }


    public void ClearCart(Guid cartId)
    {
        using var connection = CreateConnection();
        var sql = "DELETE FROM Article WHERE PanierId = @PanierId";
        connection.Execute(sql, new { PanierId = cartId });
    }

     public void UpdateItemsInCart(Guid cartId, IEnumerable<CartItem> items)
    {
        using var connection = CreateConnection();

        connection.Open();
        
        var transaction = connection.BeginTransaction();

         try
            {
                foreach (var item in items)
                {
                    var cartItem = new CartItem
                    {
                        Id = item.Id,
                        ProduitId = item.ProduitId,
                        Quantite = item.Quantite,
                        PanierId = item.PanierId
                    };
                    
                    var sql = "UPDATE CartItem SET Quantite = @Quantite,PrixUnitaire=@PrixUnintaire WHERE PanierId = @PanierId AND ProduitId = @ProduitId";
                    connection.Execute(sql, cartItem, transaction);
                }

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
    }
    public IEnumerable<CartItem> getCartItems(Guid cartId)
    {
        const string sql = @"SELECT * FROM Article WHERE PanierId=@PanierId;";
        
        using(var connection = CreateConnection())
        {
            connection.Open();
            return connection.Query<CartItem>(sql,new {PanierId=cartId}).ToList();
        }
    }

    

}
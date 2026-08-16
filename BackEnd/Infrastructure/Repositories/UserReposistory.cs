namespace Infrastructure.Repositories;

using Infrastructure.Repositories.Abstractions;
using Infrastructure.Models;
using Dapper;
using MySql.Data.MySqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;

public class UserRepository(IConfiguration configuration) : IUserRepository
{
     private readonly string _connectionString = configuration.GetConnectionString("DefaultConnection")
                            ?? throw new ArgumentNullException(nameof(configuration), "Database connection string 'DefaultConnection' not found.");

    private IDbConnection CreateConnection() => new MySqlConnection(_connectionString);
    public User? GetUserByUsername(string username)
    {
        const string sql = "SELECT Id, Username, PasswordHash, IsAdmin, Nom, Prenom FROM Utilisateur WHERE Username = @Username;";
        using var connection = CreateConnection();
        return connection.QuerySingleOrDefault<User?>(sql, new { Username = username });
    }
    public void AddUser(User user)
    {
        string sql = @"INSERT INTO Utilisateur (Username,PasswordHash,IsAdmin,Nom,Prenom) VALUES (@Username,@PasswordHash,@IsAdmin,@Nom,@Prenom)";
        
        using var connection = CreateConnection();
        connection.Execute(sql,new {user.Username,user.PasswordHash,user.IsAdmin,user.Nom,user.Prenom});
    }
    public IEnumerable<User> GetAllUsers()
    {
        const string sql = """
            SELECT Id,
                   Username,
                   PasswordHash,
                   Nom,
                   Prenom
            FROM Utilisateur;
            """;
        using var connection = CreateConnection();
        return connection.Query<User>(sql);
    }

    public bool UpdateUser(User user)
    {
        const string sql = @"
            UPDATE Utilisateur
            SET Id = @Id,
                Username= @Username,
                Nom = @Nom,
                Prenom = @Prenom,
                IdFacturation=@IdFacturation,
                IdLivraison=@IdLivraison
            WHERE Id = @Id;
            ";
        using (var connection = CreateConnection())
        {
            connection.Open();
            int affectedRaw = connection.Execute(sql,user);
            return affectedRaw>0;
        }
        
    }
}
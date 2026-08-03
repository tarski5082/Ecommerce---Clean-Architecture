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
        const string sql = "SELECT Id, Username, PasswordHash, IsAdmin, Nom, Prenom FROM Users WHERE Username = @Username;";
        using var connection = CreateConnection();
        return connection.QuerySingleOrDefault<User?>(sql, new { Username = username });
    }
    public void AddUser(User user)
    {
        string sql = @"INSERT INTO Users (Username,PasswordHash,IsAdmin,Nom,Prenom) VALUES (@Username,@PasswordHash,@IsAdmin,@Nom,@Prenom)";
        
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
            FROM Users;
            """;
        using var connection = CreateConnection();
        return connection.Query<User>(sql);
    }
}
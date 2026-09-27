using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using portfolioApi.Model;

namespace portfolioApi.Repository;

public class UserRepository : IUserRepository
{
    private readonly string _connectionString;

    public UserRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("college_db")        //Find the connection string whose name is college_db.
            ?? throw new InvalidOperationException("Connection string 'college_db' not found.");
    }

    private IDbConnection Connection => new SqlConnection(_connectionString);

    // 1. Get All Users
    public async Task<List<User>> GetAll()
    {
        try
        {
            
        using var connection = Connection;

        var users = await connection.QueryAsync<User>("sp_GetUser", commandType: CommandType.StoredProcedure);  // QueryAsync<User> == Execute a query/stored procedure asynchronously and return multiple records mapped to the User class. OR Dapper automatically maps these columns to your User properties.
        return users.ToList();
        }
        catch (Exception)
        {
            throw;
        }
    }

    // 2. Get User By UserId
    public async Task<User?> GetById(int Id)
    {
        try{
        using IDbConnection db = Connection;
        return await db.QueryFirstOrDefaultAsync<User>("sp_GetUserById", new { userId = Id }, commandType: CommandType.StoredProcedure); // QueryFirstOrDefaultAsync<User> == Execute the query asynchronously and return the first matching record as a User. If no record is found, return null.
        }
        catch (Exception)
        {
            throw;
        }
    }

    // 3. Add New User (POST)
    public async Task<User> Add(User user)
    {
        try{
        using var connection = Connection;
        var newId = await connection.ExecuteScalarAsync<int>("sp_InsertUser", new
        {
            user.FirstName,
            user.LastName,
            user.UserName,
            user.Email,
            user.Password,
            user.Address
        }, commandType: CommandType.StoredProcedure);

        user.UserId = newId;
        return user;
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while adding user.", ex);
        }

    }

    // 4. Update Existing User (PUT)
    public async Task<User?> Update(int Id, User user)
    {
        try
        {
        using IDbConnection db = Connection;
        var rowsAffected = await db.ExecuteAsync("sp_UpdateUser", new
        {
            userId = Id,
            user.FirstName,
            user.LastName,
            user.UserName,
            user.Email,
            user.Password,
            user.Address
        }, commandType: CommandType.StoredProcedure);

        return await db.QueryFirstOrDefaultAsync<User>("sp_GetUserById", new { userId = Id }, commandType: CommandType.StoredProcedure);
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while updating user.", ex);
        }
    }

    // 5. Delete User By UserId (DELETE)
    public async Task<bool> Delete(int Id)
    {
        try{

        using IDbConnection db = Connection;
        var result = await db.ExecuteAsync("sp_DeleteUser", new { userId = Id }, commandType: CommandType.StoredProcedure); //ExecuteAsync == This is a Dapper method used when you want to execute a command but don't need to retrieve a list of objects.
        return result > 0;
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while deleting user.", ex);
        }
    }
}

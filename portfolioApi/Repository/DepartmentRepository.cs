using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using portfolioApi.Model;

namespace portfolioApi.Repository;

public class DepartmentRepository : IDepartmentRepository
{
    private readonly string _connectionString;

    public DepartmentRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("college_db")
        ?? throw new InvalidOperationException("Connection string 'college_db' not found.");
    }

    private IDbConnection Connection => new SqlConnection(_connectionString);

    // 1. Get All Departments
    public async Task<List<Department>> GetAll()
    {
        try
        {
            using var connection = Connection;
            var departments = await connection.QueryAsync<Department>("sp_GetDepartment", commandType: CommandType.StoredProcedure);
            return departments.ToList();
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while fetching department records.", ex);
        }
    }

    // 2. Get Department By DepartmentId
    public async Task<Department?> GetById(int Id)
    {
        try
        {
            using IDbConnection db = Connection;
            return await db.QueryFirstOrDefaultAsync<Department>("sp_GetDepartmentById", new { departmentId = Id }, commandType: CommandType.StoredProcedure);
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while fetching department record.", ex);
        }
    }

    // 3. Add New Department (POST)
    public async Task<Department> Add(Department department)
    {
        try
        {
            using var connection = Connection;
            var newId = await connection.ExecuteScalarAsync<int>("sp_AddDepartment", new
            {
                department.DepartmentName,
                department.DepartmentCode,
                department.CreatedBy
            }, commandType: CommandType.StoredProcedure);

            department.DepartmentId = newId;
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while adding department record.", ex);
        }
        return department;
    }

    // 4. Update Existing Department (PUT)
    public async Task<Department?> Update(int Id, Department department)
    {
        try
        {
            using IDbConnection db = Connection;
            var rowsAffected = await db.ExecuteAsync("sp_UpdateDepartment", new
            {
                departmentId = Id,
                department.DepartmentName,
                department.DepartmentCode
            }, commandType: CommandType.StoredProcedure);

        if (rowsAffected == 0)
        {
            return null;
        }

        department.DepartmentId = Id;
        return department;
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while updating department record.", ex);
        }
    }

    // 5. Delete Department By DepartmentId (DELETE)
    public async Task<bool> Delete(int Id)
    {
        try
        {
            using IDbConnection db = Connection;
            var result = await db.ExecuteAsync("sp_DeleteDepartment", new { departmentId = Id }, commandType: CommandType.StoredProcedure);
            return result > 0;
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while deleting department record.", ex);
        }
    }
}

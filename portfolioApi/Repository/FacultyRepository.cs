using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using portfolioApi.Model;

namespace portfolioApi.Repository;

public class FacultyRepository : IFacultyRepository
{
    private readonly string _connectionString;

    public FacultyRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("college_db")
        ?? throw new InvalidOperationException("Connection string 'college_db' not found.");
    }

    private IDbConnection Connection => new SqlConnection(_connectionString);

    // 1. Get All Faculties
    public async Task<List<Faculty>> GetAll()
    {
        try
        {
            using var connection = Connection;
            var faculties = await connection.QueryAsync<Faculty>("sp_GetFaculty", commandType: CommandType.StoredProcedure);
            return faculties.ToList();
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while fetching faculties.", ex);
        }
    }

    // 2. Get Faculty By FacultyId
    public async Task<Faculty?> GetById(int facultyId)
    {
        try
        {
            using IDbConnection db = Connection;
            return await db.QueryFirstOrDefaultAsync<Faculty>("sp_GetFacultyById", new { facultyId = facultyId }, commandType: CommandType.StoredProcedure);
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while fetching faculty.", ex);
        }
    }

    // 3. Add New Faculty (POST)
    public async Task<Faculty> Add(Faculty faculty)
    {
        try{
        using var connection = Connection;
        var newId = await connection.ExecuteScalarAsync<int>("sp_AddFaculty", new
        {
            faculty.FirstName,
            faculty.LastName,
            faculty.Gender,
            faculty.Subjects,
            faculty.Email,
            faculty.Phone,
            faculty.Salary
        }, commandType: CommandType.StoredProcedure);

        faculty.FacultyId = newId;
        return faculty;
    }
    catch(Exception ex)
        {
            throw new Exception(
                "An error occurred while adding faculty.", ex);
        }
    }
    // 4. Update Existing Faculty (PUT)
    public async Task<Faculty?> Update(int Id, Faculty faculty)
    {
        try{
        using IDbConnection db = Connection;
        var rowsAffected = await db.ExecuteAsync("sp_UpdateFaculty", new
        {
            facultyId = Id,
            faculty.FirstName,
            faculty.LastName,
            faculty.Gender,
            faculty.Subjects,
            faculty.Email,
            faculty.Phone,
            faculty.Salary
        }, commandType: CommandType.StoredProcedure);

        if (rowsAffected == 0)
        {
            return null;
        }

        faculty.FacultyId = Id;
        return faculty;
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while updating faculty.", ex);
        }
    }

    // 5. Delete Faculty By FacultyId (DELETE)
    public async Task<bool> Delete(int facultyId)
    {
        try
        {
            using IDbConnection db = Connection;
            var result = await db.ExecuteAsync("sp_DeleteFaculty", new { facultyId = facultyId }, commandType: CommandType.StoredProcedure);
            return result > 0;
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while deleting faculty.", ex);
        }
    }
}
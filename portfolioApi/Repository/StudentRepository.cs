using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using portfolioApi.Model;

namespace portfolioApi.Repository;

public class StudentRepository : IStudentRepository
{
    private readonly string _connectionString;
    public StudentRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("college_db")
        ?? throw new InvalidOperationException("Connection string 'college_db' not found.");
    }

    private IDbConnection Connection => new SqlConnection(_connectionString);


    // 1. Get All Students
    public async Task<List<Student>> GetAll()
    {
        try
        {
            using var connection = Connection;
            var students = await connection.QueryAsync<Student>("sp_GetStudent");
            return students.ToList();
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while fetching students.", ex);
        }
    }

    // 2. Get Student By ID
    public async Task<Student?> GetById(int id)
    {
        try
        {
            using IDbConnection db = Connection;
            return await db.QueryFirstOrDefaultAsync<Student>("sp_GetStudentById", new { id = id }, commandType: CommandType.StoredProcedure);
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while fetching student.", ex);
        }
    }

    // 3. Add New Student (POST)
    public async Task<Student> Add(Student student)
    {
        try
        {
            using IDbConnection db = Connection;
            student.Prn = await db.ExecuteScalarAsync<int>(
                "sp_AddStudent",
                new
                {
                student.FirstName,
                student.LastName,
                student.Gender,
                student.DateOfBirth,
                student.Email,
                student.ContactNo,
                student.CreatedBy,
                student.CreatedOn,
                student.IsActive
            },
            commandType: CommandType.StoredProcedure);
        return student;
    }
    catch (Exception ex)
    {
        throw new Exception("An error occurred while adding student.", ex);
    }
    }


    // 4. Update Existing Student (PUT)
    public async Task<Student?> Update(Student student)
    {
        try
        {
            using IDbConnection db = Connection;
            await db.ExecuteAsync(
                "sp_UpdateStudent",
                new
                {
                student.Prn,
                student.DateOfBirth,
                student.Email,
                student.ContactNo
            },
            commandType: CommandType.StoredProcedure);

            return student;
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while updating student.", ex);
        }
    }
   
    // 5. Delete Student By ID (DELETE)
    public async Task<bool> Delete(int id)
    {
        try
        {
            using IDbConnection db = Connection;
            var result = await db.ExecuteAsync("sp_DeleteStudent", new { Id = id }, commandType: CommandType.StoredProcedure);
            return result > 0;
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while deleting student.", ex);
        }
    }
}

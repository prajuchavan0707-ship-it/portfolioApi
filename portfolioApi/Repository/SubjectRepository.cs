using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using portfolioApi.Model;

namespace portfolioApi.Repository;

public class SubjectRepository : ISubjectRepository
{
    private readonly string _connectionString;
    public SubjectRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("college_db")
        ?? throw new InvalidOperationException("Connection string 'college_db' not found.");
    }

    private IDbConnection Connection => new SqlConnection(_connectionString);
    
    // 1. Get All Subjects
    public async Task<List<Subject>> GetAll()
    {
        try
        {
            using var connection = Connection;
            var subjects = await connection.QueryAsync<Subject>("sp_GetSubject");
            return subjects.ToList();
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while fetching subjects.", ex);
        }
    }

    // 2. Get Subject By subjectId
    public async Task<Subject?> GetById(int subjectId)
    {
        try
        {
            using IDbConnection db = Connection;
            return await db.QueryFirstOrDefaultAsync<Subject>("sp_GetSubjectById", new { subjectId = subjectId }, commandType: CommandType.StoredProcedure);
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while fetching subject.", ex);
        }
    }

    // 3. Add New Subject (POST)
    public async Task<Subject> Add(Subject Subject)
    {
        try
        {
            using IDbConnection db = Connection;
            var id = await db.ExecuteScalarAsync<int>("sp_AddSubject", new
            {
                Subject.Faculty,
                Subject.CourseTitle,
                Subject.Code,
                Subject.credit
            }, commandType: CommandType.StoredProcedure);
                        return id > 0 ? await GetById(id) : throw new Exception("Failed to add subject.");
    }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while adding subject.", ex);
        }
    }

    // 4. Update Existing Subject (PUT)
    public async Task<Subject?> Update(int Id, Subject subject)
    {
        try{
       using IDbConnection db = Connection;
       await db.ExecuteAsync("sp_UpdateSubject", new
        {
            SubjectId = Id,
            subject.Faculty,
            subject.CourseTitle,
            subject.Code,
            subject.credit,
            subject.ModifyBy,
            subject.ModifyOn
        }, commandType: CommandType.StoredProcedure);
        return subject;
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while updating subject.", ex);
        }
    }

    // 5. Delete Subject By subjectId (DELETE)
    public async Task<bool> Delete(int subjectId)
    {
        try
        {
            using IDbConnection db = Connection;
            var result = await db.ExecuteAsync("sp_DeleteSubject", new { subjectId = subjectId }, commandType: CommandType.StoredProcedure);
            return result > 0;
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while deleting subject.", ex);
        }
    }

      
}



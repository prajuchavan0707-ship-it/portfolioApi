using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using portfolioApi.Model;

namespace portfolioApi.Repository;

public class AttendanceRepository : IAttendanceRepository
{
    private readonly string _connectionString;

    public AttendanceRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("college_db")
        ?? throw new InvalidOperationException("Connection string 'college_db' not found.");
    }

    private IDbConnection Connection => new SqlConnection(_connectionString);

    // 1. Get All Attendance records
    public async Task<List<Attendance>> GetAll()
    {
        try
        {
            using var connection = Connection;
            var attendances = await connection.QueryAsync<Attendance>("sp_GetAttendance", commandType: CommandType.StoredProcedure);
            return attendances.ToList();
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while fetching attendance records.", ex);
        }
    }

    // 2. Get Attendance By AttendanceId
    public async Task<Attendance?> GetById(int Id)
    {
        try
        {
            using IDbConnection db = Connection;
            return await db.QueryFirstOrDefaultAsync<Attendance>("sp_GetAttendanceById", new { attendanceId = Id }, commandType: CommandType.StoredProcedure);
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while fetching attendance record.", ex);
        }
    }

    // 3. Add New Attendance (POST)
    public async Task<Attendance> Add(Attendance attendance)
    {
        try
        {
            using var connection = Connection;
            var newId = await connection.ExecuteScalarAsync<int>("sp_AddAttendance", new
            {
                attendance.StudentPrn,
                attendance.SubjectId,
                attendance.AttendanceDate,
                attendance.Status
            }, commandType: CommandType.StoredProcedure);

            attendance.AttendanceId = newId;
            return attendance;
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while adding attendance record.", ex);
        }
    }

    // 4. Update Existing Attendance (PUT)
    public async Task<Attendance?> Update(int Id, Attendance attendance)
    {
        try
        {
            using IDbConnection db = Connection;
            var rowsAffected = await db.ExecuteAsync("sp_UpdateAttendance", new
            {
                attendanceId = Id,
                attendance.StudentPrn,
                attendance.SubjectId,
                attendance.AttendanceDate,
                attendance.Status,
                attendance.ModifyBy
            }, commandType: CommandType.StoredProcedure);

        if (rowsAffected == 0)
        {
            return null;
        }

        attendance.AttendanceId = Id;
        return attendance;
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while updating attendance record.", ex);
        }
    }

    // 5. Delete Attendance By AttendanceId (DELETE)
    public async Task<bool> Delete(int Id)
    {
        try
        {
            using IDbConnection db = Connection;
            var result = await db.ExecuteAsync("sp_DeleteAttendance", new { attendanceId = Id }, commandType: CommandType.StoredProcedure);
            return result > 0;
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while deleting attendance record.", ex);
        }
    }
}

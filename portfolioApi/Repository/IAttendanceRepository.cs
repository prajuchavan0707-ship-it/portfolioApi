using portfolioApi.Model;

namespace portfolioApi.Repository;

public interface IAttendanceRepository
{
    Task<List<Attendance>> GetAll();
    Task<Attendance?> GetById(int id);
    Task<Attendance> Add(Attendance attendance);
    Task<Attendance?> Update(int id, Attendance updatedAttendance);
    Task<bool> Delete(int id);
}

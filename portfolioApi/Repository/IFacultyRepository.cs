using portfolioApi.Model;

namespace portfolioApi.Repository;

public interface IFacultyRepository
{
    Task<List<Faculty>> GetAll();
    Task<Faculty?> GetById(int id);
    Task<Faculty> Add(Faculty faculty);
    Task<Faculty?> Update(int id, Faculty updatedFaculty);
    Task<bool> Delete(int id);
}

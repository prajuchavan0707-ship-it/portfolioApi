using portfolioApi.Model;

namespace portfolioApi.Repository;

public interface IDepartmentRepository
{
    Task<List<Department>> GetAll();
    Task<Department?> GetById(int id);
    Task<Department> Add(Department department);
    Task<Department?> Update(int id, Department updatedDepartment);
    Task<bool> Delete(int id);
}

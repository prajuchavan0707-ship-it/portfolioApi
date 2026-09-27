using portfolioApi.Model;

namespace portfolioApi.Repository;

public interface IStudentRepository
{
    Task<List<Student>> GetAll();
    Task<Student?> GetById(int id);
    Task<Student> Add(Student student);
    Task<Student?> Update(Student student);
    Task<bool> Delete(int id);
}

using portfolioApi.Model;

namespace portfolioApi.Repository;

public interface ISubjectRepository
{
    Task<List<Subject>> GetAll();
    Task<Subject?> GetById(int id);
    Task<Subject> Add(Subject subject);
    Task<Subject?> Update(int id, Subject updatedSubject);
    Task<bool> Delete(int id);
}

using portfolioApi.Model;

namespace portfolioApi.Repository;

public interface IUserRepository
{
    Task<List<User>> GetAll();
    Task<User?> GetById(int id);
    Task<User> Add(User user);
    Task<User?> Update(int id, User updatedUser);
    Task<bool> Delete(int id);
}

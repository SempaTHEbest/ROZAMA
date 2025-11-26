using Rozetka.Core.Models;

namespace Rozetka.Core.Interfaces;

public interface IUserRepository
{
    Task<User?> GetById(Guid id);
    Task<IEnumerable<User>> GetAll();
    Task Add(User user);
    Task Update(User user);
    Task Delete(Guid id);
}
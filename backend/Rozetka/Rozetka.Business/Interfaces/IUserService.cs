using Rozetka.Core.Models;

namespace Rozetka.Business.Interfaces;

public interface IUserService
{
    Task<User?> GetById(Guid id);
    Task<IEnumerable<User>> GetAll();
    Task<User> Create(string firstName, string lastName, string email);
    Task Update(Guid id, string firstName, string lastName, string email);
    Task Delete(Guid id);
}
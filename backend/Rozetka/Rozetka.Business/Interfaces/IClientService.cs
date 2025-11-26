using Rozetka.Core.Models;

namespace Rozetka.Business.Interfaces;

public interface IClientService
{
    Task<Client> GetById(Guid id);
    Task<IEnumerable<Client>> GetAll();
    Task<Client> Create(string firstName, string lastName, string address);
    Task Update(Guid id, string firstName, string lastName, string address);
    Task Delete(Guid id);
}
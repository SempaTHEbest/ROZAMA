using Rozetka.Core.Models;

namespace Rozetka.Core.Interfaces;

public interface IClientRepository
{
    Task<Client?> GetById(Guid id);
    Task<IEnumerable<Client>> GetAll();
    Task Add(Client client);
    Task Update(Client client);
    Task Delete(Guid id);
}
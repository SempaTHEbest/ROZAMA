using Rozetka.Core.Models;

namespace Rozetka.Core.Interfaces;

public interface IOrderRepository
{
    Task<Order?> GetById(Guid id);
    Task<IEnumerable<Order>> GetAll();
    Task Add(Order order);
    Task Update(Order order);
    Task Delete(Guid id);
}
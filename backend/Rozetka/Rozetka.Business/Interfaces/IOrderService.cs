using Rozetka.Core.Models;

namespace Rozetka.Business.Interfaces;

public interface IOrderService
{
    Task<Order?> GetById(Guid id);
    Task<IEnumerable<Order>> GetAll();
    Task<Order> Create(string number, decimal total,Guid clientId);
    Task Update(Guid id, string status, decimal total);
    Task Delete(Guid id);
}
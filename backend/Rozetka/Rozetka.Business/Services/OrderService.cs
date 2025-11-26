using Rozetka.Business.Interfaces;
using Rozetka.Core.Interfaces;
using Rozetka.Core.Models;

namespace Rozetka.Business.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;

    public OrderService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<Order> Create(string number, decimal total, Guid clientId)
    {
        if(total<0) throw new ArgumentException("Total must be greater than or equal to zero");
        if(string.IsNullOrWhiteSpace(number)) throw new ArgumentException("Number cannot be empty");

        var order = new Order
        {
            Id = Guid.NewGuid(),
            Number = number,
            Total = total,
            ClientId = clientId,
            Status = "New",
            CreatedAt = DateTime.Now,
        };

        await _orderRepository.Add(order);
        return order;
    }

    public async Task Update(Guid id, string status, decimal total)
    {
        var existingOrder = await _orderRepository.GetById(id);

        if (existingOrder == null)
        {
            throw new KeyNotFoundException($"Order with id {id} not found");
        }
        if (total<0) throw new ArgumentException("Total must be greater than or equal to zero");
        
        existingOrder.Total = total;
        existingOrder.Status = status;
        await _orderRepository.Update(existingOrder);
    }

    public async Task Delete(Guid id)
    {
        await _orderRepository.Delete(id);
    }

    public async Task<IEnumerable<Order>> GetAll()
    {
        return await _orderRepository.GetAll();
    }

    public async Task<Order> GetById(Guid id)
    {
        return await _orderRepository.GetById(id);
    }
}
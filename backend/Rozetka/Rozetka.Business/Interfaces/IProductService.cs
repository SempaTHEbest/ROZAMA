using Rozetka.Core.Models;

namespace Rozetka.Business.Interfaces;

public interface IProductService
{
    Task<Product> Create(string code, string name, decimal price);
    Task<IEnumerable<Product>> GetAll();
    Task<Product?> GetById(Guid id);
    Task Update(Guid id, string code, string name, decimal price);
    Task Delete(Guid id);
}
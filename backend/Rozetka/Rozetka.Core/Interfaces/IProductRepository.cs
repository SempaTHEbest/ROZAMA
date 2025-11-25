using Rozetka.Core.Models;

namespace Rozetka.Core.Interfaces;

public interface IProductRepository
{
    Task<Product?> GetById(Guid id);
    Task<IEnumerable<Product>> GetAll();
    Task Add(Product product);
    Task Update(Product product);
    Task Delete(Guid id);
}
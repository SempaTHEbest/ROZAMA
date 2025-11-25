using Rozetka.Business.Interfaces;
using Rozetka.Core.Interfaces;
using Rozetka.Core.Models;

namespace Rozetka.Business.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Product> Create(string code, string name, decimal price)
    {
        if (price < 0) throw new ArgumentException("Price cannot be negative");

        var product = new Product
        {
            Id = Guid.NewGuid(),
            Code = code,
            Name = name,
            Price = price
        };
        await _productRepository.Add(product);
        return product;
    }

    public async Task<IEnumerable<Product>> GetAll()
    {
        return await _productRepository.GetAll();
    }

    public async Task<Product?> GetById(Guid id)
    {
        return await _productRepository.GetById(id);
    }

    public async Task Update(Guid id, string code, string name, decimal price)
    {
        var existingProduct = await _productRepository.GetById(id);

        if (existingProduct == null)
        {
            throw new KeyNotFoundException($"Product with ID {id} not found.");
        }

        if (price < 0) throw new ArgumentException("Price cannot be negative");

        existingProduct.Code = code;
        existingProduct.Name = name;
        existingProduct.Price = price;

        await _productRepository.Update(existingProduct);
    }

    public async Task Delete(Guid id)
    {
        await _productRepository.Delete(id);
    }
}
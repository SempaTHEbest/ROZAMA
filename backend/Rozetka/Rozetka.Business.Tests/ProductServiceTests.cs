using Moq;
using Rozetka.Business.Services;
using Rozetka.Core.Interfaces;
using Rozetka.Core.Models;
namespace Rozetka.Business.Tests;

[TestFixture]
public class ProductServiceTests
{
    private Mock<IProductRepository> _mockRepo;
    private ProductService _service;
    [SetUp]
    public void Setup()
    {
        _mockRepo = new Mock<IProductRepository>();
        _service = new ProductService(_mockRepo.Object);
    }

    [Test]
    public async Task Create_Product()
    {
        //Arrange
        
        string code = "code";
        string name = "Iphone 15";
        decimal price = 900;
        
        //Act
        
        var result = await _service.Create(code, name, price);
        
        //Assert
        
        _mockRepo.Verify(x => x.Add(It.IsAny<Product>()), Times.Once);
        Assert.That(result.Name, Is.EqualTo(name));
        Assert.That(result.Id, Is.Not.EqualTo(Guid.Empty));
        
    }

    [Test]
    public async Task Update_Product_Success()
    {
        //Arrange
        var id = Guid.NewGuid();
        var existingProduct = new Product
        {
            Id = id,
            Code = "code",
            Name = "Iphone 15",
            Price = 100
        };
        _mockRepo.Setup(x => x.GetById(id))
            .ReturnsAsync(existingProduct);
        //Act
        await _service.Update(id, "NewCode",  "NewName", 300);
        
        //Assert
        _mockRepo.Verify(x => x.Update(existingProduct), Times.Once);
        Assert.That(existingProduct.Code, Is.EqualTo("NewCode"));
        Assert.That(existingProduct.Name, Is.EqualTo("NewName"));
        Assert.That(existingProduct.Price, Is.EqualTo(300));
    }

    [Test]
    public async Task Delete_Product_Success()
    {
        //Arrange
        var id = Guid.NewGuid();
        //Act
        await _service.Delete(id);
        //Assert
        _mockRepo.Verify(x => x.Delete(id), Times.Once);
    }

    [Test]
    public async Task GetById_Product_Success()
    {
        //Arrange
        var  id = Guid.NewGuid();
        var product = new Product
        {
            Id = id
        };
        _mockRepo.Setup(x => x.GetById(id)).ReturnsAsync(product);
        //Act
        var result = await _service.GetById(id);
        //Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Id, Is.EqualTo(id));
    }

    [Test]
    public async Task GetAll_Products_Success()
    {
        //Arrange
        var fakeList = new List<Product> {new  Product(), new Product()};
        _mockRepo.Setup(x => x.GetAll())
            .ReturnsAsync(fakeList);
        //Act
        var result = await _service.GetAll();
        //Assert
        Assert.That(result.Count(), Is.EqualTo(fakeList.Count));
    }
    
    
}

//Arrange
//Act
//Assert
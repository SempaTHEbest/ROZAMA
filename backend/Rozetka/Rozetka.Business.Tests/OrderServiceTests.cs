using Moq;
using Rozetka.Business.Services;
using Rozetka.Core.Interfaces;
using Rozetka.Core.Models;
namespace Rozetka.Business.Tests;

[TestFixture]
public class OrderServiceTests
{
    private Mock<IOrderRepository> _mockRepo;
    private OrderService _service;
    
    [SetUp]
    public void Setup()
    {
        _mockRepo = new Mock<IOrderRepository>();
        _service = new OrderService(_mockRepo.Object);
    }
    [Test]
    public async Task Create_Order()
    {
        //Arrange
        string number = "GHGH-23313";
        decimal total = 100;
        Guid clientId = Guid.NewGuid();
        //Act
        var result = await _service.Create(number, total, clientId);
        //Assert
        _mockRepo.Verify(x => x.Add(It.IsAny<Order>()), Times.Once);
        Assert.That(result.Status, Is.EqualTo("New"));
        Assert.That(result.Total, Is.EqualTo(total));
        Assert.That(result.ClientId, Is.EqualTo(clientId));
        Assert.That(result.Number, Is.EqualTo(number));
        Assert.That(result.Id, Is.Not.EqualTo(Guid.Empty));
    }

    [Test]
    public async Task Update_Order()
    {
        //Arrange
        var id = Guid.NewGuid();
        var existingOrder = new Order
        {
            Id = id,
            Status = "New",
            Total = 100,
        };
        _mockRepo.Setup(x => x.GetById(id)).ReturnsAsync(existingOrder);
        
        //Act
        await _service.Update(id, "Paid", 200);
        //Assert
        _mockRepo.Verify(x => x.Update(existingOrder), Times.Once);
        Assert.That(existingOrder.Status, Is.EqualTo("Paid"));
        Assert.That(existingOrder.Total, Is.EqualTo(200));
    }

    [Test]
    public async Task Delete_Order()
    {
        //Arrange
        var id = Guid.NewGuid();
        //Act
        await _service.Delete(id);
        //Assert
        _mockRepo.Verify(x => x.Delete(id), Times.Once);
    }

    [Test]
    public async Task GetAll_Orders()
    {
        //Arrange
        var fakeList = new List<Order> { new Order(),  new Order() };
        _mockRepo.Setup(x => x.GetAll()).ReturnsAsync(fakeList);
        //Act
        var result = await _service.GetAll();
        //Assert
        Assert.That(result.Count(), Is.EqualTo(fakeList.Count));
    }

    [Test]
    public async Task GetById_Order()
    {
        //Arrange
        var id = Guid.NewGuid();
        var order = new Order{Id = id};
        _mockRepo.Setup(x => x.GetById(id)).ReturnsAsync(order);
        //Act
        var result = await _service.GetById(id);
        //Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Id, Is.EqualTo(order.Id));

    }
}
//Arrange
//Act
//Assert
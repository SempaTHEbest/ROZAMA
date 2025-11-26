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
}
//Arrange
//Act
//Assert
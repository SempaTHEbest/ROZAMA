using Moq;
using Rozetka.Business.Services;
using Rozetka.Core.Interfaces;
using Rozetka.Core.Models;

namespace Rozetka.Business.Tests;

[TestFixture]
public class ClientServiceTests
{
    private Mock<IClientRepository> _mockRepo;
    private ClientService _service;

    [SetUp]
    public void Setup()
    {
        _mockRepo = new Mock<IClientRepository>();
        _service = new ClientService(_mockRepo.Object);
    }

    [Test]
    public async Task Create_Client()
    {
        //Arrange
        string firstName = "John";
        string lastName = "Doe";
        string address = "NY";
        //Act
        var result = await _service.Create(firstName, lastName, address);
        //Assert
        _mockRepo.Verify(x => x.Add(It.IsAny<Client>()),  Times.Once);
        Assert.That(result.FirstName, Is.EqualTo(firstName));
        Assert.That(result.LastName, Is.EqualTo(lastName));
        Assert.That(result.Address, Is.EqualTo(address));
        Assert.That(result.Id, Is.Not.EqualTo(Guid.Empty));
    }

    [Test]
    public async Task Update_Client()
    {
        //Arrange
        var id = Guid.NewGuid();
        var existingClient = new Client
        {
            Id = id,
            FirstName = "John",
            LastName = "Doe",
            Address = "NY",
        };
        _mockRepo.Setup(x => x.GetById(id)).ReturnsAsync(existingClient);
        
        //Act
        await _service.Update(id, "NewName", "NewLast" ,"NewAddress");
        
        //Assert
        _mockRepo.Verify(x => x.Update(existingClient),  Times.Once);
        Assert.That(existingClient.FirstName, Is.EqualTo("NewName"));
        Assert.That(existingClient.LastName, Is.EqualTo("NewLast"));
        Assert.That(existingClient.Address, Is.EqualTo("NewAddress"));
    }

    [Test]
    public async Task Delete_Client()
    {
        //Arrange
        var id = Guid.NewGuid();
        //Act
        await _service.Delete(id);
        //Assert
        _mockRepo.Verify(x => x.Delete(id), Times.Once);
    }

    [Test]
    public async Task GetById_Client()
    {
        //Arrange
        var id = Guid.NewGuid();
        var client = new Client{Id = id, FirstName = "John"};
        _mockRepo.Setup(x => x.GetById(id)).ReturnsAsync(client);
        //Act
        var result = await _service.GetById(id);
        //Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.FirstName, Is.EqualTo(client.FirstName));
    }

    [Test]
    public async Task GetAll_Client()
    {
        //Arrange
        var fakeList = new List<Client> {new Client(),  new Client()};
        _mockRepo.Setup(x => x.GetAll()).ReturnsAsync(fakeList);
        //Act
        var result = await _service.GetAll();
        //Assert
        Assert.That(result.Count, Is.EqualTo(fakeList.Count));
    }
}
using Moq;
using Rozetka.Business.Services;
using Rozetka.Core.Interfaces;
using Rozetka.Core.Models;
namespace Rozetka.Business.Tests;

[TestFixture]
public class UserServiceTests
{
    private Mock<IUserRepository> _mockRepo;
    private UserService _service;
    
    [SetUp]
    public void Setup()
    {
        _mockRepo = new Mock<IUserRepository>();
        _service = new UserService(_mockRepo.Object);
    }

    [Test]
    public async Task Create_User()
    {
        //Arrange
        string first = "Admin";
        string last = "User";
        string email = "admin@rozetka.com";
        
        //Act
        var result = await _service.Create(first, last, email);
        
        //Assert
        _mockRepo.Verify(x => x.Add(It.IsAny<User>()),  Times.Once);
        
        Assert.That(result.FirstName, Is.EqualTo(first));
        Assert.That(result.LastName, Is.EqualTo(last));
        Assert.That(result.Email, Is.EqualTo(email));
    }
    
    [Test]
    public async Task Update_User()
    {
        //Arrange
        var id = Guid.NewGuid();
        var existingUser = new User
        {
            Id = id,
            FirstName = "Admin",
            LastName = "User",
            Email = "admin@rozetka.com"
        };
        
        _mockRepo.Setup(x => x.GetById(id)).ReturnsAsync(existingUser);
        
        //Act
        await _service.Update(id, "New",  "New", "New");
        
        //Assert
        _mockRepo.Verify(x => x.Update(existingUser), Times.Once);
        Assert.That(existingUser.FirstName, Is.EqualTo("New"));
        Assert.That(existingUser.LastName, Is.EqualTo("New"));
        Assert.That(existingUser.Email, Is.EqualTo("New"));
    }

    [Test]
    public async Task Delete_User()
    {
        //Arrange
        var id = Guid.NewGuid();
        //Act
        await _service.Delete(id);
        //Assert
        _mockRepo.Verify(x => x.Delete(id), Times.Once);
    }

    [Test]
    public async Task GetAll_Users()
    {
        var fakeList = new List<User> {new User(),  new User()};
        _mockRepo.Setup(x => x.GetAll()).ReturnsAsync(fakeList);
        
        var result = await _service.GetAll();
        
        Assert.That(result.Count, Is.EqualTo(fakeList.Count));
    }

    [Test]
    public async Task GetUserById()
    {
        var id = Guid.NewGuid();
        var user = new  User{Id =id, FirstName = "Admin", LastName = "User", Email = "rozetka@gmail.com"};
        _mockRepo.Setup(x => x.GetById(id)).ReturnsAsync(user);
        
        var result = await _service.GetById(id);
        
        Assert.That(result, Is.Not.Null);
        Assert.That(result.FirstName, Is.EqualTo(user.FirstName));
        Assert.That(result.LastName, Is.EqualTo(user.LastName));
        Assert.That(result.Email, Is.EqualTo(user.Email));
    }
}
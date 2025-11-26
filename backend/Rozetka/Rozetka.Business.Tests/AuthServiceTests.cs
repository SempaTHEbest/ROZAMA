using Moq;
using Rozetka.Business.Services;
using Rozetka.Core.Interfaces;
using Rozetka.Core.Models;

namespace Rozetka.Business.Tests;

[TestFixture]
public class AuthServiceTests
{
    private Mock<IAuthRepository> _mockRepo;
    private AuthService _service;
    
    [SetUp]
    public void SetUp()
    {
        _mockRepo = new Mock<IAuthRepository>();
        _service = new AuthService(_mockRepo.Object);
    }

    [Test]
    public async Task Register_User()
    {
        //Arrange
        string username = "user";
        string password = "password";
        
        _mockRepo.Setup(x => x.GetByUserName(username)).ReturnsAsync((AuthUser?)null);
        
        //Act
        var result = await _service.Register(username, password);
        
        //Assert
        _mockRepo.Verify(x => x.Add(It.IsAny<AuthUser>()),  Times.Once);
        
        Assert.That(result.UserName, Is.EqualTo(username));
        Assert.That(result.PasswordHash, Is.Not.EqualTo(password));
        Assert.That(result.PasswordSalt, Is.Not.Empty);
    }

    [Test]
    public async Task Login_Test()
    {
        //Arrange
        string username = "user";
        string password = "password";
        var helperService = new AuthService(new Mock<IAuthRepository>().Object);
        
        AuthUser? savedUser = null;
        _mockRepo.Setup(x => x.Add(It.IsAny<AuthUser>()))
            .Callback<AuthUser>(u => savedUser = u);
        _mockRepo.Setup(x => x.GetByUserName(username))
            .ReturnsAsync((AuthUser?)null);
        
        await _service.Register(username, password);
        _mockRepo.Setup(x => x.GetByUserName(username)).ReturnsAsync(savedUser);
        
        //Act
        var isSuccess = await _service.Login(username, password);
        //Assert
        Assert.That(isSuccess, Is.True);
    }
}
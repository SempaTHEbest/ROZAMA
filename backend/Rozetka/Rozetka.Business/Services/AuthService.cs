using Rozetka.Business.Interfaces;
using Rozetka.Core.Interfaces;
using Rozetka.Core.Models;
using System.Security.Cryptography;
using System.Text;


namespace Rozetka.Business.Services;

public class AuthService : IAuthService
{
    private readonly IAuthRepository _authRepository;

    public AuthService(IAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }

    public async Task<AuthUser> Register(string username, string password)
    {
        if (await _authRepository.GetByUserName(username) != null)
        {
            throw new Exception("Username already exists");
        }

        if (string.IsNullOrWhiteSpace(password))
        {
            throw new ArgumentException("Password is required");
        }
        
        using var hmac = new HMACSHA512();

        var user = new AuthUser
        {
            Id = Guid.NewGuid(),
            UserName = username,
            PasswordSalt = hmac.Key,
            PasswordHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(password))),
            TokenExpirationDate = DateTime.UtcNow.AddDays(7)
        };
        
        await _authRepository.Add(user);
        return user;
    }

    public async Task<bool> Login(string username, string password)
    {
        var user = await _authRepository.GetByUserName(username);
        if (user == null)
        {
            return false;
        }
        
        using var hmac = new HMACSHA512(user.PasswordSalt);
        
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        var computedHashString = Convert.ToBase64String(computedHash);
        
        return computedHashString == user.PasswordHash;
    }
}
using Rozetka.Core.Models;

namespace Rozetka.Business.Interfaces;

public interface IAuthService
{
    Task<AuthUser> Register(string userName, string password);
    Task<bool> Login(string userName, string password);
}
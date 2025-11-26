using Rozetka.Core.Models;

namespace Rozetka.Core.Interfaces;

public interface IAuthRepository
{
    Task<AuthUser?> GetByUserName(string userName);
    Task Add(AuthUser user);
}
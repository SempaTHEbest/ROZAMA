namespace Rozetka.Core.Models;

public class AuthUser
{
    public Guid Id { get;}
    public string UserName { get;}
    public string PasswordHash { get;}
    public string PasswordSalt { get;}
    public string TokenExpirationDate { get;}
}
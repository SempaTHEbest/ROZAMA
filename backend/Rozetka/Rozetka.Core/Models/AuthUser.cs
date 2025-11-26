namespace Rozetka.Core.Models;

public class AuthUser
{
    public Guid Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string PasswordHash { get; set; } =  string.Empty;
    public byte[] PasswordSalt { get; set; } = Array.Empty<byte>();
    public DateTime TokenExpirationDate { get; set; }
}
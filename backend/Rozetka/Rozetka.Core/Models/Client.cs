namespace Rozetka.Core.Models;

public class Client
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Adress { get; set; } = string.Empty;
}
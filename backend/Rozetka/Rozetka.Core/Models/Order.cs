namespace Rozetka.Core.Models;

public class Order
{
    public Guid Id { get; set; }
    public string Number { get; set; } =  string.Empty;
    public decimal Total { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public Guid ClientId  { get; set; }
}
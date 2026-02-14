using System.ComponentModel.DataAnnotations;

namespace payment_service.Models;

public class Transaction
{
    [Key]
    public Guid Id { get; set; }
    
    // These link to the other services logically
    public Guid RideId { get; set; }
    public Guid RiderId { get; set; }
    
    public double Amount { get; set; }
    public string Status { get; set; } = "Pending"; // Success, Failed, Pending
    public string TransactionReference { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
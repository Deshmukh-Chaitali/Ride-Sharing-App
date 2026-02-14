namespace payment_service.Dtos;

public class PaymentRequestDto
{
    public Guid RideId { get; set; }
    public Guid RiderId { get; set; }
    public double Amount { get; set; }
    public string PaymentMethod { get; set; } = "UPI"; // CreditCard, Wallet, etc.
}
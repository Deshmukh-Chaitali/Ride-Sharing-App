using payment_service.Dtos;

namespace payment_service.Services;

public class PaymentGateway : IPaymentGateway
{
    public async Task<(bool Success, string Reference)> ChargeAsync(double amount, string method)
    {
        // Simulate network delay to a bank (e.g., Razorpay/Stripe)
        await Task.Delay(1500);

        // Logic: Simulate failure if amount is negative or suspiciously high
        if (amount <= 0 || amount > 10000)
        {
            return (false, "DECLINED");
        }

        // Generate a random mock reference string
        string mockRef = "PAY-" + Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
        return (true, mockRef);
    }
}
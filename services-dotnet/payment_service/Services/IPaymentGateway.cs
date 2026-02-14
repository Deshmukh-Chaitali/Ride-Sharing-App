using payment_service.Dtos;

namespace payment_service.Services;

public interface IPaymentGateway
{
    // Returns a tuple: (IsSuccess, TransactionReference)
    Task<(bool Success, string Reference)> ChargeAsync(double amount, string method);
}
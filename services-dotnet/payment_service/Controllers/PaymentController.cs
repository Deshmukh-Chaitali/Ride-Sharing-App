using Microsoft.AspNetCore.Mvc;
using payment_service.Data;
using payment_service.Dtos;
using payment_service.Models;
using payment_service.Services;

namespace payment_service.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentController : ControllerBase
{
    private readonly IPaymentGateway _paymentGateway;
    private readonly PaymentDbContext _context;

    public PaymentController(IPaymentGateway paymentGateway, PaymentDbContext context)
    {
        _paymentGateway = paymentGateway;
        _context = context;
    }

    [HttpPost("process")]
    public async Task<IActionResult> ProcessPayment([FromBody] PaymentRequestDto request)
    {
        // 1. Create a record with "Pending" status
        var transaction = new Transaction
        {
            Id = Guid.NewGuid(),
            RideId = request.RideId,
            RiderId = request.RiderId,
            Amount = request.Amount,
            Status = "Pending"
        };

        // 2. Attempt to charge via the Gateway (Mock)
        var result = await _paymentGateway.ChargeAsync(request.Amount, request.PaymentMethod);

        // 3. Update status based on bank response
        if (result.Success)
        {
            transaction.Status = "Success";
            transaction.TransactionReference = result.Reference;
        }
        else
        {
            transaction.Status = "Failed";
            transaction.TransactionReference = "DECLINED";
        }

        // 4. Save to Database
        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();

        // 5. Return result
        if (transaction.Status == "Success")
        {
            return Ok(transaction);
        }

        return BadRequest(new { Message = "Payment processing failed", Transaction = transaction });
    }

    [HttpGet("history/{riderId}")]
    public IActionResult GetRiderHistory(Guid riderId)
    {
        var history = _context.Transactions
            .Where(t => t.RiderId == riderId)
            .OrderByDescending(t => t.CreatedAt)
            .ToList();
            
        return Ok(history);
    }
}
namespace admin_service.Dtos;

public class PricingDto
{
    public double BaseFare { get; set; } = 50.0;
    public double RatePerKm { get; set; } = 12.0;
    public double PriceMultiplier { get; set; } = 1.0;
}
namespace admin_service.Dtos;

public class PricingDto
{
    public double BaseFare { get; set; } = 50.0; //fix for first 3 km
    public double RatePerKm { get; set; } = 12.0;  //will be used for extra km after 3 km
    public double PriceMultiplier { get; set; } = 1.0; // For Surge Pricing (multiplying factor to change price evaluation as per demand )
}
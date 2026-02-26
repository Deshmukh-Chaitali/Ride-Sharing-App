using admin_service.Dtos;

namespace admin_service.Services;

public class PricingService : IPricingService
{
    // Default values
    private PricingDto _currentPricing = new PricingDto();

    //gives current pricing values
    public PricingDto GetCurrentPricing() => _currentPricing;

    //can update pricing values
    public void UpdatePricing(PricingDto newPricing)
    {
        _currentPricing = newPricing;
    }

    //doubles pricemultiplier and base fare
    public void SetSurgeMode(bool isRainy)
    {
        if (isRainy)
        {
            _currentPricing.PriceMultiplier = 2.0;
            _currentPricing.BaseFare = 100.0; // Double base fare
        }
        else
        {
            _currentPricing.PriceMultiplier = 1.0;
            _currentPricing.BaseFare = 50.0; // normal Fare 
        }
    }   

    public double CalculateEstimatedFare(double distance)
{
    double fare = _currentPricing.BaseFare;

    if (distance > 3)
    {
        double extraKm = distance - 3;
        fare += (extraKm * _currentPricing.RatePerKm);
    }

    return fare * _currentPricing.PriceMultiplier;
}
}
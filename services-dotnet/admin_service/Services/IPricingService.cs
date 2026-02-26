using admin_service.Dtos;

namespace admin_service.Services;

public interface IPricingService
{
    PricingDto GetCurrentPricing();
    void UpdatePricing(PricingDto newPricing);
    void SetSurgeMode(bool isRainy);
    
    // THIS LINE FIXES THE ERROR:
    double CalculateEstimatedFare(double distance);
}
using admin_service.Dtos;

namespace admin_service.Services;

public interface IPricingService
{
    //to get current pricing for price evaluation
    PricingDto GetCurrentPricing();

    //to upadate values accordingly
    void UpdatePricing(PricingDto newPricing);

    //doubles the pricing in rainy season
    void SetSurgeMode(bool isRainy); 
}
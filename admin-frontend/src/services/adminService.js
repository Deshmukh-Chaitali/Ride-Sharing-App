import axios from 'axios';

// IMPORTANT: .NET [Route("api/[controller]")] makes the URL /api/Admin (Capital A)
const DOTNET_ADMIN_URL = "http://localhost:5241/api/Admin"; 
const JAVA_RIDE_URL = "http://localhost:8081/api/ride";

export const adminService = {
    // 1. Fetch Stats (Users, Drivers, Riders) from .NET
    getStats: () => axios.get(`${DOTNET_ADMIN_URL}/stats`),
    
    // 2. Fetch All Rides from Java (The one you just fixed with the /all mapping)
    getAllRides: () => axios.get(`${JAVA_RIDE_URL}/all`),

    // 3. Get Current Pricing configuration from .NET
    getPricing: () => axios.get(`${DOTNET_ADMIN_URL}/pricing`),

    // 4. Update Pricing (BaseFare, RatePerKm)
    updatePricing: (pricingData) => axios.post(`${DOTNET_ADMIN_URL}/pricing/update`, pricingData),

    // 5. Get a quick fare estimate for the dashboard
    getEstimate: (distance) => axios.get(`${DOTNET_ADMIN_URL}/estimate?distance=${distance}`)
};
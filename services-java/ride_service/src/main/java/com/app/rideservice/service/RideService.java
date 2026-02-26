package com.app.rideservice.service;

import java.util.List;
import java.util.Map;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.stereotype.Service;
import org.springframework.web.client.RestTemplate;

import com.app.rideservice.dto.RideRequest;
import com.app.rideservice.model.Ride;
import com.app.rideservice.repository.RideRepository;

@Service
public class RideService {

    @Autowired
    private RideRepository rideRepository;

    @Autowired
    private RestTemplate restTemplate;

    private final String USER_SERVICE_URL = "http://localhost:5228/api/user/";
    private final String ADMIN_SERVICE_URL = "http://localhost:5241/api/Admin/estimate?distance=";

    public List<Ride> getAllRides() {
        return rideRepository.findAll();
    }

    public List<Ride> getAvailableRides() {
        return rideRepository.findByStatus("REQUESTED");
    }

    public Ride createRide(RideRequest request) {
        // ... (Your existing validation logic)
        Double fare = 150.0; 
        try {
            ResponseEntity<Map> pricingResponse = restTemplate.getForEntity(ADMIN_SERVICE_URL + request.getDistance(), Map.class);
            if (pricingResponse.getBody() != null && pricingResponse.getBody().containsKey("estimatedFare")) {
                fare = Double.valueOf(pricingResponse.getBody().get("estimatedFare").toString());
            }
        } catch (Exception e) {
            System.out.println("Pricing fetch failed: " + e.getMessage());
        }

        Ride ride = new Ride();
        ride.setRiderId(request.getRiderId());
        ride.setPickupLocation(request.getPickupLocation());
        ride.setDestination(request.getDestination());
        ride.setDistance(request.getDistance());
        ride.setFare(fare);
        ride.setStatus("REQUESTED");

        return rideRepository.save(ride);
    }

    public Ride acceptRide(Long rideId, Long driverId) {
        Ride ride = rideRepository.findById(rideId)
            .orElseThrow(() -> new RuntimeException("Ride not found"));
        ride.setDriverId(driverId);
        ride.setStatus("ACCEPTED");
        return rideRepository.save(ride);
    }
}
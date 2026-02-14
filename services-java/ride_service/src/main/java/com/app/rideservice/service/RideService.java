package com.app.rideservice.service;

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

    public Ride createRide(RideRequest request) {
        // 1. Cross-Service Call: Validate user in .NET User Service
        String userServiceUrl = "http://localhost:5001/api/user/" + request.getRiderId();
        
        try {
            ResponseEntity<Object> response = restTemplate.getForEntity(userServiceUrl, Object.class);
            if (!response.getStatusCode().is2xxSuccessful()) {
                throw new RuntimeException("Invalid Rider ID");
            }
        } catch (Exception e) {
            throw new RuntimeException("User Service unavailable or Rider not found");
        }

        // 2. Simple Fare Calculation (Placeholder logic)
        Double fare = 150.0; 

        // 3. Save Ride
        Ride ride = new Ride();
        ride.setRiderId(request.getRiderId());
        ride.setPickupLocation(request.getPickupLocation());
        ride.setDestination(request.getDestination());
        ride.setFare(fare);
        ride.setStatus("PENDING");

        return rideRepository.save(ride);
    }
}
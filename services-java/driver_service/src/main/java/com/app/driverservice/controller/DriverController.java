package com.app.driverservice.controller;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable; // Crucial for cross-service calls
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.PutMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;
import org.springframework.web.client.RestTemplate;

import com.app.driverservice.dto.DriverDTO;
import com.app.driverservice.model.Driver;
import com.app.driverservice.repository.DriverRepository;

@RestController
@RequestMapping("/api/drivers")
public class DriverController {

    @Autowired
    private DriverRepository driverRepository;

    @Autowired
    private RestTemplate restTemplate;

    // URL of the Ride Service (Port 8081)
    private final String RIDE_SERVICE_URL = "http://localhost:8081/api/ride";

    // --- Profile Management (Local to this service) ---

    @PutMapping("/{id}/status")
    public ResponseEntity<Driver> updateStatus(@PathVariable Long id, @RequestParam boolean available) {
        Driver driver = driverRepository.findById(id)
                .orElseThrow(() -> new RuntimeException("Driver not found"));
        driver.setAvailable(available);
        return ResponseEntity.ok(driverRepository.save(driver));
    }

    // --- Ride Management (Communicates with Ride Service) ---

    /**
     * Fetch available rides from the Ride Service
     */
    @GetMapping("/available-rides")
    public ResponseEntity<List> getAvailableRides() {
        // We call the Ride Service's endpoint
        List availableRides = restTemplate.getForObject(RIDE_SERVICE_URL + "/available", List.class);
        return ResponseEntity.ok(availableRides);
    }

    /**
     * Accept a ride by calling the Ride Service
     */
    @PostMapping("/{driverId}/accept-ride/{rideId}")
    public ResponseEntity<Object> acceptRide(@PathVariable Long driverId, @PathVariable Long rideId) {
        // Check if driver exists in our DB first
        Driver driver = driverRepository.findById(driverId)
                .orElseThrow(() -> new RuntimeException("Driver profile not found"));

        if (!driver.isAvailable()) {
            throw new RuntimeException("Driver is currently offline");
        }

        // Forward the acceptance to Ride Service
        String url = RIDE_SERVICE_URL + "/" + rideId + "/accept";
        // Passing driverId in the body as requested by your RideController logic
        ResponseEntity<Object> response = restTemplate.postForEntity(url, driverId, Object.class);
        
        return ResponseEntity.ok(response.getBody());
    }

    @PostMapping("/register")
public ResponseEntity<Driver> registerDriver(@RequestBody DriverDTO dto) {
    Driver driver = new Driver();
    driver.setName(dto.getName());
    driver.setVehicleType(dto.getVehicleType());
    driver.setLicensePlate(dto.getLicensePlate());
    driver.setCurrentCity(dto.getCurrentCity());
    driver.setAvailable(true); // Default to online
    driver.setRating(5.0);     // Initial rating
    
    return ResponseEntity.ok(driverRepository.save(driver));
}
}
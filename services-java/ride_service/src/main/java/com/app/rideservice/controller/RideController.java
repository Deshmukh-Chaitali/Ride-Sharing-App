package com.app.rideservice.controller;

import java.util.List;
import java.util.Map; // Added for flexible response

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.CrossOrigin; // Simplified imports
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RestController;

import com.app.rideservice.dto.RideRequest;
import com.app.rideservice.model.Ride;
import com.app.rideservice.service.RideService;

@RestController
@RequestMapping("/api/ride")
// CHANGE: Allow both 3000 and 3001 to avoid CORS errors during development
@CrossOrigin(origins = "*", allowedHeaders = "*", methods = {RequestMethod.GET, RequestMethod.POST})
public class RideController {

    @Autowired
    private RideService rideService;

    // 1. Booking a ride
    @PostMapping("/book")
    public ResponseEntity<Ride> bookRide(@RequestBody RideRequest request) {
        System.out.println("Received booking request for: " + request.getDestination());
        return ResponseEntity.ok(rideService.createRide(request));
    }

    // 2. Accept a ride (Improved to handle simple JSON)
    @PostMapping("/{id}/accept")
    public ResponseEntity<Ride> acceptRide(@PathVariable Long id, @RequestBody Map<String, Long> payload) {
        Long driverId = payload.get("driverId");
        return ResponseEntity.ok(rideService.acceptRide(id, driverId));
    }

    // 3. Driver sees available rides
    @GetMapping("/available")
    public ResponseEntity<List<Ride>> getAvailableRides() {
        return ResponseEntity.ok(rideService.getAvailableRides());
    }

    // 4. Admin or Debug: Get all rides
    // @GetMapping("/all")
    // public ResponseEntity<List<Ride>> getAllRides() {
    //     return ResponseEntity.ok(rideService.getAllRides());
    // }
}
package com.app.rideservice.controller;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import com.app.rideservice.dto.RideRequest;
import com.app.rideservice.model.Ride;
import com.app.rideservice.service.RideService;

@RestController
@RequestMapping("/api/ride")
public class RideController {

    @Autowired
    private RideService rideService;

    @PostMapping("/book")
    public ResponseEntity<Ride> bookRide(@RequestBody RideRequest request) {
        return ResponseEntity.ok(rideService.createRide(request));
    }

    @GetMapping("/{id}")
    public ResponseEntity<Ride> getRideDetails(@PathVariable Long id) {
        // Logic to fetch ride by ID
        return ResponseEntity.ok().build(); 
    }
}
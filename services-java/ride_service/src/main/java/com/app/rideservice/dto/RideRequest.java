package com.app.rideservice.dto;

import lombok.Data;

@Data
public class RideRequest {
    private Long riderId;
    private String pickupLocation;
    private String destination;
}
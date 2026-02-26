package com.app.rideservice.dto;

public class RideRequest {
    private Long riderId;
    private String pickupLocation;
    private String destination;
    private double distance;

    // 1. MUST HAVE: Default No-Args Constructor
    public RideRequest() {
    }

    // 2. Optional: Parameterized Constructor
    public RideRequest(Long riderId, String pickupLocation, String destination, double distance) {
        this.riderId = riderId;
        this.pickupLocation = pickupLocation;
        this.destination = destination;
        this.distance = distance;
    }

    // 3. MUST HAVE: Getters and Setters (Spring uses these to map JSON)
    public Long getRiderId() {
        return riderId;
    }

    public void setRiderId(Long riderId) {
        this.riderId = riderId;
    }

    public String getPickupLocation() {
        return pickupLocation;
    }

    public void setPickupLocation(String pickupLocation) {
        this.pickupLocation = pickupLocation;
    }

    public String getDestination() {
        return destination;
    }

    public void setDestination(String destination) {
        this.destination = destination;
    }

    public double getDistance() {
        return distance;
    }

    public void setDistance(double distance) {
        this.distance = distance;
    }

    // 4. Good for debugging: toString
    @Override
    public String toString() {
        return "RideRequest{" +
                "riderId=" + riderId +
                ", pickupLocation='" + pickupLocation + '\'' +
                ", destination='" + destination + '\'' +
                ", distance=" + distance +
                '}';
    }
}
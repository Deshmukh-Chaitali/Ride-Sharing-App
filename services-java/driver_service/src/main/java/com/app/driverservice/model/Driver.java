package com.app.driverservice.model;

import jakarta.persistence.Entity;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.GenerationType;
import jakarta.persistence.Id;
import jakarta.persistence.Table;

@Entity
@Table(name = "drivers")
public class Driver {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;
    
    private String name;
    private String vehicleType;
    private String licensePlate;
    private String currentCity;
    private boolean isAvailable; 
    private Double rating;

    // Default Constructor (Required by JPA)
    public Driver() {}

    // All-Args Constructor
    public Driver(Long id, String name, String vehicleType, String licensePlate, String currentCity, boolean isAvailable, Double rating) {
        this.id = id;
        this.name = name;
        this.vehicleType = vehicleType;
        this.licensePlate = licensePlate;
        this.currentCity = currentCity;
        this.isAvailable = isAvailable;
        this.rating = rating;
    }

    // Getters and Setters
    public Long getId() { return id; }
    public void setId(Long id) { this.id = id; }

    public String getName() { return name; }
    public void setName(String name) { this.name = name; }

    public String getVehicleType() { return vehicleType; }
    public void setVehicleType(String vehicleType) { this.vehicleType = vehicleType; }

    public String getLicensePlate() { return licensePlate; }
    public void setLicensePlate(String licensePlate) { this.licensePlate = licensePlate; }

    public String getCurrentCity() { return currentCity; }
    public void setCurrentCity(String currentCity) { this.currentCity = currentCity; }

    public boolean isAvailable() { return isAvailable; }
    public void setAvailable(boolean available) { isAvailable = available; }

    public Double getRating() { return rating; }
    public void setRating(Double rating) { this.rating = rating; }
}
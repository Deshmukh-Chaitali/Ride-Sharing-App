package com.app.driverservice.dto;

public class DriverDTO {
    private String name;
    private String vehicleType;
    private String licensePlate;
    private String currentCity;

    // Constructors
    public DriverDTO() {}

    public DriverDTO(String name, String vehicleType, String licensePlate, String currentCity) {
        this.name = name;
        this.vehicleType = vehicleType;
        this.licensePlate = licensePlate;
        this.currentCity = currentCity;
    }

    // Getters and Setters
    public String getName() { return name; }
    public void setName(String name) { this.name = name; }

    public String getVehicleType() { return vehicleType; }
    public void setVehicleType(String vehicleType) { this.vehicleType = vehicleType; }

    public String getLicensePlate() { return licensePlate; }
    public void setLicensePlate(String licensePlate) { this.licensePlate = licensePlate; }

    public String getCurrentCity() { return currentCity; }
    public void setCurrentCity(String currentCity) { this.currentCity = currentCity; }
}
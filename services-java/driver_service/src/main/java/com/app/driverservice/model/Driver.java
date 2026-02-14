package com.app.driverservice.model;

import jakarta.persistence.Entity;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.GenerationType;
import jakarta.persistence.Id;
import jakarta.persistence.Table;
import lombok.Data;

@Entity
@Table(name = "drivers")
@Data
public class Driver {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;
    
    private String name;
    private String vehicleType; // e.g., Sedan, SUV
    private String licensePlate;
    private String currentCity;
    private boolean isAvailable; // Crucial for the Ride Service to find them
    private Double rating;
}
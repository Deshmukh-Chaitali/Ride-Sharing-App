package com.app.driverservice.repository;

import java.util.List;

import org.springframework.data.jpa.repository.JpaRepository;

import com.app.driverservice.model.Driver;

public interface DriverRepository extends JpaRepository<Driver, Long> {
    // Find all drivers in a city who are currently online
    List<Driver> findByCurrentCityAndIsAvailableTrue(String city);
}
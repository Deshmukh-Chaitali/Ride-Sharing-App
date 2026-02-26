package com.app.driverservice.repository;

import java.util.List;

import org.springframework.data.jpa.repository.JpaRepository;

import com.app.driverservice.model.Driver;

public interface DriverRepository extends JpaRepository<Driver, Long> {
    // This allows you to find active drivers for a specific location
    List<Driver> findByCurrentCityAndIsAvailableTrue(String city);
}
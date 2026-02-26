package com.app.rideservice;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.context.annotation.Bean;
import org.springframework.web.client.RestTemplate;

@SpringBootApplication
public class RideServiceApplication {

    public static void main(String[] args) {
        SpringApplication.run(RideServiceApplication.class, args);
    }

    // This Bean allows the RideService to make HTTP requests to the .NET Admin Service
    @Bean
    public RestTemplate restTemplate() {
        return new RestTemplate();
    }
}
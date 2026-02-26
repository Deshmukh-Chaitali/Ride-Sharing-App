package com.app.driverservice;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.context.annotation.Bean;
import org.springframework.web.client.RestTemplate;

@SpringBootApplication
public class DriverServiceApplication {

    public static void main(String[] args) {
        SpringApplication.run(DriverServiceApplication.class, args);
    }

    /**
     * This Bean is required so that Spring can inject RestTemplate 
     * into your DriverController for cross-service communication.
     */
    @Bean
    public RestTemplate restTemplate() {
        return new RestTemplate();
    }
}
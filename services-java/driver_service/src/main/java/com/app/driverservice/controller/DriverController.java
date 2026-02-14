@PutMapping("/{id}/status")
public ResponseEntity<Driver> updateStatus(@PathVariable Long id, @RequestParam boolean available) {
    Driver driver = driverRepository.findById(id).orElseThrow();
    driver.setAvailable(available);
    return ResponseEntity.ok(driverRepository.save(driver));
}
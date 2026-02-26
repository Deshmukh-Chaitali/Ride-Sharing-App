import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { MapContainer, TileLayer, Marker, Popup } from 'react-leaflet';
import L from 'leaflet';
import 'leaflet/dist/leaflet.css';
import './RiderDashboard.css';

// Fix Leaflet Icon
delete L.Icon.Default.prototype._getIconUrl;
L.Icon.Default.mergeOptions({
    iconRetinaUrl: 'https://cdnjs.cloudflare.com/ajax/libs/leaflet/1.7.1/images/marker-icon-2x.png',
    iconUrl: 'https://cdnjs.cloudflare.com/ajax/libs/leaflet/1.7.1/images/marker-icon.png',
    shadowUrl: 'https://cdnjs.cloudflare.com/ajax/libs/leaflet/1.7.1/images/marker-shadow.png',
});

const RiderDashboard = () => {
    const [pickup, setPickup] = useState('My Location');
    const [destination, setDestination] = useState('');
    const [fare, setFare] = useState(null);
    const [loading, setLoading] = useState(false);
    const [coords, setCoords] = useState({ lat: 20.7453, lng: 78.6022 });
    const [currentDistance, setCurrentDistance] = useState(0);

    useEffect(() => {
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(
                (pos) => setCoords({ lat: pos.coords.latitude, lng: pos.coords.longitude }),
                (err) => console.log("Location denied, using default.")
            );
        }
    }, []);

    const calculateDistance = (lat1, lon1, lat2, lon2) => {
        const R = 6371; 
        const dLat = (lat2 - lat1) * Math.PI / 180;
        const dLon = (lon2 - lon1) * Math.PI / 180;
        const a = 
            Math.sin(dLat/2) * Math.sin(dLat/2) +
            Math.cos(lat1 * Math.PI / 180) * Math.cos(lat2 * Math.PI / 180) * Math.sin(dLon/2) * Math.sin(dLon/2);
        const c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1-a));
        return R * c; 
    };

  const handleGetEstimate = async () => {
    if (!destination) {
        alert("Please enter a destination!");
        return;
    }

    setLoading(true);
    try {
        // 1. Get Coordinates with proper Headers
        const geoRes = await axios.get(
            `https://nominatim.openstreetmap.org/search?format=json&q=${encodeURIComponent(destination)}`,
            {
                headers: {
                    'User-Agent': 'MyRideApp/1.0' // Identifies your app to Nominatim
                }
            }
        );

        if (!geoRes.data || geoRes.data.length === 0) {
            alert("City not found. Try 'Pune' or 'Mumbai'.");
            setLoading(false);
            return;
        }

        const destLat = parseFloat(geoRes.data[0].lat);
        const destLon = parseFloat(geoRes.data[0].lon);
        
        const dist = calculateDistance(coords.lat, coords.lng, destLat, destLon);
        setCurrentDistance(dist);

        // 2. Call .NET Admin Service
        const response = await axios.get(`http://localhost:5241/api/Admin/estimate?distance=${dist}`);
        setFare(Math.round(response.data.EstimatedFare || response.data.estimatedFare));

    } catch (error) {
        console.error("Detailed Error:", error);
        if (error.response) {
            // Server responded with an error (like 403 or 429)
            alert(`API Error: ${error.response.status}. Nominatim might be busy.`);
        } else {
            // Network error (blocked by browser or internet down)
            alert("Network Error: Connection to Geocoding API was blocked. Check Console (F12).");
        }
    } finally {
        setLoading(false);
    }
};

    const handleConfirmRide = async () => {
        setLoading(true);
        try {
            const rideData = {
                riderId: 1, 
                pickupLocation: pickup,
                destination: destination,
                distance: currentDistance 
            };
            const response = await axios.post('http://localhost:8080/api/ride/book', rideData);
            alert("Ride Booked! ID: " + (response.data.id || "Success"));
            setFare(null);
            setDestination('');
        } catch (error) {
            alert("Java Service Error (Port 8080)!");
        } finally {
            setLoading(false);
        }
    };

    return (
        <div className="mobile-container">
            <div className="map-container-style">
                {/* use of react leaflet  */}
                <MapContainer center={[coords.lat, coords.lng]} zoom={13} style={{ height: '100%', width: '100%' }}>
                    <TileLayer url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png" />
                    <Marker position={[coords.lat, coords.lng]} />
                </MapContainer>
            </div>
            
            <div className="request-card">
                <h3>Request a Ride</h3>
                <input type="text" value={pickup} onChange={(e) => setPickup(e.target.value)} placeholder="Pickup" />
                <input type="text" value={destination} onChange={(e) => setDestination(e.target.value)} placeholder="Where to?" />

                {!fare ? (
                    <button className="primary-btn" onClick={handleGetEstimate} disabled={loading}>
                        {loading ? "Calculating..." : "Check Prices"}
                    </button>
                ) : (
                    <div className="fare-display">
                        <p>Estimated Fare: <strong>₹{fare}</strong></p>
                        <button className="book-btn" onClick={handleConfirmRide}>Confirm Ride</button>
                        <button className="secondary-btn" onClick={() => setFare(null)}>Back</button>
                    </div>
                )}
            </div>
        </div>
    );
};

export default RiderDashboard;
import React, { useEffect, useState } from 'react';
import { adminService } from '../services/adminService';

const Dashboard = () => {
    const [rides, setRides] = useState([]);
    const [stats, setStats] = useState({ totalRides: 0 });

    useEffect(() => {
        // Load data from both .NET and Java when the page opens
        adminService.getStats().then(res => setStats(res.data));
        adminService.getAllRides().then(res => setRides(res.data));
    }, []);

    return (
        <div style={{ padding: '20px' }}>
            <h2>Admin Dashboard</h2>
            <div style={{ background: '#eee', padding: '15px', borderRadius: '8px' }}>
                <strong>Total Rides in System:</strong> {stats.totalRides}
            </div>

            <h3>Recent Ride Requests (Live from Java Service)</h3>
            <table border="1" width="100%" cellPadding="10">
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Pickup</th>
                        <th>Destination</th>
                        <th>Status</th>
                    </tr>
                </thead>
                <tbody>
                    {rides.map(ride => (
                        <tr key={ride.id}>
                            <td>{ride.id}</td>
                            <td>{ride.pickupLocation}</td>
                            <td>{ride.destination}</td>
                            <td>{ride.status}</td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
};

export default Dashboard;
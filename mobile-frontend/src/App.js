import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import RiderDashboard from './pages/RiderDashboard';
import './App.css';

function App() {
  return (
    <Router>
      <div className="App">
        <Routes>
          {/* Default route shows the Rider Dashboard */}
          <Route path="/" element={<RiderDashboard />} />
          
          {/* We will add /driver and /login routes here later */}
        </Routes>
      </div>
    </Router>
  );
}

export default App;
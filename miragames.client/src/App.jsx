import { BrowserRouter as Router, Routes, Route, Link } from 'react-router-dom';
import Login from './Pages/LoginPage';
import Dashboard from './Pages/DashboardPage';
import Home from './Pages/HomePage';

function App() {
    return (
        <Router>
            <nav>
                <Link to="/">Home</Link>
                <Link to="/login">Login</Link>
                <Link to="/dashboard">Dashboard</Link>
            </nav>

            <Routes>
                <Route path="/" element={<Home />} />
                <Route path="/login" element={<Login />} />
                <Route path="/dashboard" element={<Dashboard />} />
            </Routes>
        </Router>
    );
}

export default App;
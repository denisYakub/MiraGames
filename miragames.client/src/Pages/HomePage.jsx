import { useEffect } from 'react';
import { useNavigate } from 'react-router-dom';

export default function HomePage() {
    const navigate = useNavigate();

    useEffect(() => {
        const token = localStorage.getItem('token');
        if (!token) {
            navigate('/login');
            return;
        }

        fetch('/api/ping/auth', {
            method: 'GET',
            headers: {
                'Authorization': `Bearer ${token}`,
                "Content-Type": "application/json"
            }
        }).then((res) => {
            if (res.status === 200) {
                navigate('/dashboard');
            } else {
                navigate('/login');
            }
        }).catch(() =>
            navigate('/login')
        );
    }, [navigate]);

    return <div className="Card">Checking auth...</div>;
}
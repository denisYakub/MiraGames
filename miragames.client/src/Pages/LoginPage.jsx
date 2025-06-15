import { useState } from "react"
import { useNavigate } from 'react-router-dom';

export default function Login() {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState('');
    const navigate = useNavigate();

    const handleRegistration = async (e) => {
        e.preventDefault();
        setError('');

        await fetch('/api/auth/login', {
            method: 'POST',
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({email, password})
        }).then(async (res) => {
            if (res.status === 200) {
                const token = await res.json();
                localStorage.setItem('token', token.result);
                navigate('/dashboard');
            } else {
                setError('Unexpected response:', res);
                console.error(res);
            }
        }).catch((ex) => {
            setError('Unexpected error:', ex);
            console.error(ex);
        });
    }

    return (<div className="Container">
        <div className="Card">
            <h2>Registration</h2>
            <form onSubmit={handleRegistration}>
                <input type="email" placeholder="Email"
                    value={email} onChange={(e) => setEmail(e.target.value)}
                    required />
                <br />
                <input type="password" placeholder="Password"
                    value={password} onChange={(e) => setPassword(e.target.value)}
                    required />
                <br />
                <button type="submit">
                    Login
                </button>
            </form>
            {error && <p style={{ color: 'red' }}>{error}</p>}
        </div>
    </div>)
}
import { useEffect, useState } from "react";
import { useNavigate } from 'react-router-dom';

const currencies = [{ id: 1, name: 'Ruble' }];

export default function Dashboard() {
    const [clients, setClients] = useState([]);
    const [curValue, setCurValue] = useState([]);
    const [newValue, setNewValue] = useState(0);
    const [selectedCurrency, setSelectedCurrency] = useState(currencies[0]);

    const navigate = useNavigate();

    const token = localStorage.getItem('token');

    async function loadRates() {
        if (!token) {
            navigate('/login');
            return;
        }

        try {
            const curV = await Promise.all(
                currencies.map(async (cur) => {
                    const res = await fetch(`/api/rate?currency=${cur.id}`, {
                        method: 'GET',
                        headers: {
                            Authorization: `Bearer ${token}`,
                            'Content-Type': 'application/json'
                        }
                    });
                    if (res.status === 200) {
                        const result = await res.json();
                        return result.result;
                    } else {
                        console.error('Unexpected response:', res);
                        return null;
                    }
                })
            );
            setCurValue(curV);
        } catch (ex) {
            console.error('Unexpected error:', ex);
        }
    }

    useEffect(() => {
        if (!token) {
            navigate('/login');
            return;
        }

        async function fetchData() {
            try {
                const resClients = await fetch('api/clients', {
                    method: 'GET',
                    headers: {
                        Authorization: `Bearer ${token}`,
                        "Content-Type": "application/json"
                    }
                });

                if (resClients.status === 200) {
                    const result = await resClients.json();
                    console.log(result.result);
                    setClients(result.result);
                } else {
                    console.error('Unexpected response:', resClients);
                }
            } catch (ex) {
                console.error('Unexpected error:', ex);
            }
        }

        fetchData();
        loadRates();
    }, [token, navigate]);

    const handleRate = async (e) => {
        e.preventDefault();

        if (!token) {
            navigate('/login');
            return;
        }

        try {
            const res = await fetch('/api/rate', {
                method: 'POST',
                headers: {
                    "Content-Type": "application/json",
                    'Authorization': `Bearer ${token}`
                },
                body: JSON.stringify({ currency: selectedCurrency.id, newValue })
            });

            if (res.status === 200) {
                await loadRates();
            } else {
                console.error('Error updating rate:', res);
            }
        } catch (ex) {
            console.error('Unexpected error:', ex);
        }
    }

    function PaymentSumPaid({ user, rubRate }) {
        const sumPaid = user.payments
            .filter(payment => payment.status === 0)
            .reduce((acc, payment) => acc + payment.price, 0);

        const converted = rubRate ? (sumPaid / rubRate).toFixed(2) : '...';

        return (
            <div>
                Paid payments sum for user: {converted}
            </div>
        );
    }

    return (
        <div className="Container">
            <div className="Card">
                <h2>Clients</h2>
                <ul>
                    {clients.map(client => (
                        <li key={client.id}>
                            {client.email}
                            <PaymentSumPaid user={client} rubRate={curValue[0]} />
                            <br />
                        </li>
                    ))}
                </ul>

                <h2>Rates</h2>
                <ul>
                    {currencies.map((currency, index) => (
                        <li key={currency.id}>
                            {currency.name}
                            <br />
                            {curValue[index] ?? 'loading...'}
                        </li>
                    ))}
                </ul>

                <form onSubmit={handleRate}>
                    <select
                        value={selectedCurrency.id}
                        onChange={(e) => {
                            const cur = currencies.find(c => c.id === Number(e.target.value));
                            setSelectedCurrency(cur);
                        }}
                    >
                        {currencies.map(currency => (
                            <option key={currency.id} value={currency.id}>
                                {currency.name}
                            </option>
                        ))}
                    </select>
                    <br />
                    <input
                        type="number"
                        placeholder="New Value"
                        value={newValue}
                        onChange={(e) => setNewValue(Number(e.target.value))}
                        required
                    />
                    <br />
                    <button type="submit">Change</button>
                </form>
            </div>
        </div>
    );
}

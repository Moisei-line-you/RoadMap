import { useState } from 'react';
import { useNavigate, Link } from 'react-router-dom';
import { login } from '../api/auth';
import { saveToken } from '../store/auth';

export default function LoginPage() {
  const navigate = useNavigate();

    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');

    const [error, setError] = useState<string | null>(null);
    const [loading, setLoading] = useState(false);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

        try {
        setLoading(true);
        setError(null);

        const response = await login(username, password);

        const token = response.token;

        saveToken(token);

        navigate('/roadmaps');

        } catch (err: any) {

        setError(
            err?.response?.data?.message || 'Something went wrong'
        );

        } finally {
            setLoading(false);
        }
  };

  return (
    <div>
        <h1>Login</h1>

        <form onSubmit={handleSubmit}>
        <input
          placeholder="Username"
          value={username}
          onChange={(e) => setUsername(e.target.value)}
        />

        <input
          type="password"
          placeholder="Password"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
        />


        {error && <p style={{ color: 'red' }}>{error}</p>}

        <button disabled={loading}>
          {loading ? 'Loading...' : 'Login'}
        </button>
      </form>

        <p>
        Do not have an account? <Link to="/register">Register</Link>
      </p>
    </div>
  );
}
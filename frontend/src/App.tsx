import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';
import { QueryClient, QueryClientProvider } from '@tanstack/react-query';
import LoginPage    from './pages/LoginPage';
import RegisterPage from './pages/RegisterPage';
import RoadmapPage  from './pages/RoadmapPage';
import RoadmapsPage from './pages/RoadmapsPage';
import { isLoggedIn } from './store/auth';

const queryClient = new QueryClient();

function PrivateRoute({ children }: { children: React.ReactNode }) {
    if (!isLoggedIn()) {
    return <Navigate to="/login" replace />;
  }

  return children;
}

export default function App() {
  return (
        <QueryClientProvider client={queryClient}>
      <BrowserRouter>
        <Routes>
          <Route path="/login" element={<LoginPage />} />
          <Route path="/register" element={<RegisterPage />} />

          <Route
            path="/roadmaps"
            element={
              <PrivateRoute>
                <RoadmapsPage />
              </PrivateRoute>
            }
          />

          <Route
            path="/roadmaps/:id"
            element={
              <PrivateRoute>
                <RoadmapPage />
              </PrivateRoute>
            }
          />

          <Route
            path="*"
            element={
              <Navigate
                to={isLoggedIn() ? '/roadmaps' : '/login'}
              />
            }
          />
        </Routes>
      </BrowserRouter>
    </QueryClientProvider>
  );
}
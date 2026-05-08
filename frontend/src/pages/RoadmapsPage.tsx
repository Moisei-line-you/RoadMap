import { useEffect, useState } from 'react';
import { getRoadmaps } from '../api/roadmaps';
import RoadmapCard from '../components/RoadmapCard';

export default function RoadmapsPage() {
  const [roadmaps, setRoadmaps] = useState<any[]>([]);

  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchRoadmaps = async () => {
      try {
        setLoading(true);
        setError(null);

        const data = await getRoadmaps();

        setRoadmaps(data);
      } catch (err) {
        setError('Failed to load roadmaps');
      } finally {
        setLoading(false);
      }
    };

    fetchRoadmaps();
  }, []);

  if (loading) return <p>Loading...</p>;

  if (error) {
    return <p style={{ color: 'red' }}>{error}</p>;
  }

  return (
    <div style={{ padding: '20px' }}>
      <h1>Roadmaps</h1>

      {roadmaps.length === 0 && (
        <p>No roadmaps found</p>
      )}

      {roadmaps.map((roadmap) => (
        <RoadmapCard
          key={roadmap.id}
          id={roadmap.id}
          title={roadmap.title}
          description={roadmap.description}
        />
      ))}
    </div>
  );
}
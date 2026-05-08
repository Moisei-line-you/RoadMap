import { Link } from 'react-router-dom';

interface RoadmapCardProps {
  id: number;
  title: string;
  description: string;
}

export default function RoadmapCard({
  id,
  title,
  description,
}: RoadmapCardProps) {
  return (
    <div
      style={{
        border: '1px solid #ccc',
        padding: '16px',
        borderRadius: '10px',
        marginBottom: '16px',
      }}
    >
      <h2>{title}</h2>

      <p>{description}</p>

      <Link to={`/roadmaps/${id}`}>
        <button>Open roadmap</button>
      </Link>
    </div>
  );
}
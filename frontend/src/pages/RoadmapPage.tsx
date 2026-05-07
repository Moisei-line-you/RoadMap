import { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import { getRoadmap } from '../api/roadmaps';
import ProgressBar from '../components/ProgressBar';
import { getProgress, markNodeComplete, unmarkNodeComplete } from '../api/progress';
import NodeCard from '../components/NodeCard';

export default function RoadmapPage() {
  const { id } = useParams();
  const roadmapId = Number(id);

  const [roadmap, setRoadmap] = useState<any>(null);
  const [completedNodeIds, setCompletedNodeIds] = useState<number[]>([]);

  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    if (!id) return;

    const fetchData = async () => {
      try {
        setLoading(true);
        setError(null);

        const [roadmapRes, progressRes] = await Promise.all([
          getRoadmap(roadmapId),
          getProgress(roadmapId),
        ]);

        setRoadmap(roadmapRes);
        setCompletedNodeIds(progressRes.completedNodeIds || []);
      } catch (err: any) {
        setError('Failed to load roadmap');
      } finally {
        setLoading(false);
      }
    };

    fetchData();
  }, [id]);

  const handleToggle = async (nodeId: number) => {
    try {
      const isCompleted = completedNodeIds.includes(nodeId);

      if (isCompleted) {
        await unmarkNodeComplete(roadmapId, nodeId);
        setCompletedNodeIds((prev) =>
          prev.filter((id) => id !== nodeId)
        );
      } else {
        await markNodeComplete(roadmapId, nodeId);
        setCompletedNodeIds((prev) => [...prev, nodeId]);
      }
    } catch (err) {
      alert('Something went wrong');
    }
  };

  if (loading) return <p>Loading...</p>;

  if (error) return <p style={{ color: 'red' }}>{error}</p>;

  if (!roadmap) return <p>Roadmap not found</p>;

    const totalNodes = roadmap.nodes?.length || 0;
    const completedCount = completedNodeIds.length;

  return (
    <div>
      <h1>{roadmap.title}</h1>
      <p>{roadmap.description}</p>

        <ProgressBar
        completed={completedCount}
        total={totalNodes}
        />

      {roadmap.nodes?.map((node: any) => (
        <NodeCard
          key={node.id}
          nodeId={node.id}
          title={node.title}
          description={node.description}
          difficulty={node.difficulty}
          isOptional={node.isOptional}
          isCompleted={completedNodeIds.includes(node.id)}
          onToggle={handleToggle}
        />
      ))}
    </div>
  );
}
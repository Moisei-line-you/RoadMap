interface NodeCardProps {
  nodeId: number;
  title: string;
  description: string;
  difficulty: number;   // 1–5
  isOptional: boolean;
  isCompleted: boolean;              // is this node already done?
  onToggle: (nodeId: number) => void; // called when the button is clicked
}

export default function NodeCard(props: NodeCardProps) {
    const {
    nodeId,
    title,
    description,
    difficulty,
    isOptional,
    isCompleted,
    onToggle,
  } = props;

  return (
  <div
      style={{
        border: isCompleted ? '2px solid green' : '1px solid #ccc',
        backgroundColor: isCompleted ? '#eaffea' : 'white',
        padding: '12px',
        borderRadius: '8px',
        marginBottom: '10px',
      }}
    >
      <h3>
        {title} {isOptional && <span>(Optional)</span>}
      </h3>

      <p>{description}</p>

      <div>
        {Array.from({ length: 5 }).map((_, index) => (
          <span key={index}>
            {index < difficulty ? '★' : '☆'}
          </span>
        ))}
      </div>

      <button onClick={() => onToggle(nodeId)}>
        {isCompleted ? '✓ Done' : 'Mark done'}
      </button>
    </div>
  );
}
interface ProgressBarProps {
  completed: number;
  total: number;
}

export default function ProgressBar({ completed, total }: ProgressBarProps) {
  const percent = total ? (completed / total) * 100 : 0;

  return (
    <div>
      <p>
        Progress: {completed}/{total} ({percent.toFixed(0)}%)
      </p>

      <div
        style={{
          width: '100%',
          height: '10px',
          backgroundColor: '#e0e0e0',
          borderRadius: '5px',
          marginTop: '10px',
          overflow: 'hidden',
        }}
      >
        <div
          style={{
            width: `${percent}%`,
            height: '100%',
            backgroundColor: 'green',
            transition: 'width 0.3s ease',
          }}
        />
      </div>
    </div>
  );
}
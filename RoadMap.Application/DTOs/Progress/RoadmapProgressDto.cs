namespace RoadMap.Application.DTOs.Progress;

public record RoadmapProgressDto(
    int RoadmapId,
    int TotalNodes,
    int CompletedCount,
    int PercentComplete,   
    List<CompletedNodeDto> CompletedNodes
);
    
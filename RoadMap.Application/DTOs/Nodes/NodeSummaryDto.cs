namespace RoadMap.Application.DTOs.Nodes;

public record NodeSummaryDto(
    int Id,
    string Title,
    string Description,
    int Difficulty,
    bool IsOptional,
    List<int> DependsOnIds
);
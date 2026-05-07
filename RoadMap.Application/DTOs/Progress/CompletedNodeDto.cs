namespace RoadMap.Application.DTOs.Progress;

public record CompletedNodeDto(
    int NodeId,
    string NodeTitle,
    DateTime CompletedAt
);
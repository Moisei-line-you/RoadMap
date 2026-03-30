namespace RoadMap.Application.DTOs.Roadmaps;

public record RoadmapDto(
    int Id,
    string Title,
    string Description,
    List<RoadmapNodeDto> Nodes
);

public record RoadmapNodeDto(
    int NodeId,
    string NodeTitle,
    double PositionX,
    double PositionY
);
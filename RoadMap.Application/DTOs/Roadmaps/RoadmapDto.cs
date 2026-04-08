using RoadMap.Domain.Enums;

namespace RoadMap.Application.DTOs.Roadmaps;

public record RoadmapDto(
    int Id,
    string Title,
    string Description,
    List<RoadmapNodeDto> Nodes
);
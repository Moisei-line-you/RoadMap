using RoadMap.Domain.Enums;

namespace RoadMap.Application.DTOs.Roadmaps;

public record RoadmapDto(
    int Id,
    string Title,
    string Description,
    List<RoadmapNodeDto> Nodes
);

public record RoadmapNodeDto(
    int NodeId,
    double PositionX,
    double PositionY
);

public record ResourceDto(
    int Id,
    string Title,
    string Url,
    ResourceType Type,
    bool IsFree
);

public record AddNodeToRoadmapRequest(
    int RoadmapId,
    int NodeId,
    double X,
    double Y
);

public record GetAvailableNodesRequest(
    int RoadmapId,
    List<int> CompletedNodeIds
);

public record CreateResourceRequest(
    string Title,
    string Url,
    ResourceType Type,
    bool IsFree
);

public record CreateRoadmapRequest(
    string Title,
    string Description);
using RoadMap.Domain.Enums;

namespace RoadMap.Application.DTOs.Nodes;

public record NodeDto(
    int Id,
    string Title,
    string Description
);

public record CreateNodeRequest(
    string Title,
    string Description,
    NodeType Type,
    int Difficulty,
    bool IsOptional
);

public record AddDependencyRequest(
    int FromNodeId,
    int ToNodeId,
    DependencyType Type
);

public record AddResourceRequest(
    int NodeId,
    int ResourceId
);
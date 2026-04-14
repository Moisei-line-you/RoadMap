using RoadMap.Domain.Enums;

namespace RoadMap.Application.DTOs.Nodes;

public record AddDependencyRequest(    
    int FromNodeId,
    int ToNodeId,
    DependencyType Type);
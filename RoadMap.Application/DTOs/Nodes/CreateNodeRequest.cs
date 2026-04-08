using RoadMap.Domain.Enums;

namespace RoadMap.Application.DTOs.Nodes;

public record CreateNodeRequest(
    string Title,
    string Description,
    NodeType Type,
    int Difficulty,
    bool IsOptional);
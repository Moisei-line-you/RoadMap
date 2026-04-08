using RoadMap.Domain.Enums;

namespace RoadMap.Application.DTOs.Resources;

public record CreateResourceRequest(
    string Title,
    string Url,
    ResourceType Type,
    bool IsFree
    );
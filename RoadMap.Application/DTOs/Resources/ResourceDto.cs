using RoadMap.Domain.Enums;

namespace RoadMap.Application.DTOs.Resources;

public record ResourceDto(   
    int Id,
    string Title,
    string Url,
    ResourceType Type,
    bool IsFree);
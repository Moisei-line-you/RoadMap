namespace RoadMap.Application.DTOs.Roadmaps;

public record AddNodeToRoadmapRequest(    
    int RoadmapId,
    int NodeId,
    double X,
    double Y);
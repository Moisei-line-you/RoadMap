namespace RoadMap.Application.DTOs.Nodes;

public record GetAvailableNodesRequest(
    int RoadmapId,
    List<int> CompletedNodeIds
    );
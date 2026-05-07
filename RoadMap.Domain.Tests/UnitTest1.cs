using RoadMap.Domain.Enums;
using RoadMap.Domain.Models.Roadmaps;
using RoadMap.Domain.Services;

namespace RoadMap.Domain.Tests;

public class DependencyGraphServiceTests
{
    [Fact]
    public void HasCycle_ReturnsTrue_WhenDependencyIntroducesCycle()
    {
        var node1 = new Node { Id = 1 };
        var node2 = new Node { Id = 2 };
        var node3 = new Node { Id = 3 };

        node1.AddDependency(node2, DependencyType.Required);
        node2.AddDependency(node3, DependencyType.Required);

        var service = new DependencyGraphService();
        var result = service.HasCycle(new[] { node1, node2, node3 }, fromNodeId: 3, toNodeId: 1);

        Assert.True(result);
    }

    [Fact]
    public void HasCycle_ReturnsFalse_WhenNoCycleExists()
    {
        var node1 = new Node { Id = 1 };
        var node2 = new Node { Id = 2 };
        var node3 = new Node { Id = 3 };

        node1.AddDependency(node2, DependencyType.Required);

        var service = new DependencyGraphService();
        var result = service.HasCycle(new[] { node1, node2, node3 }, fromNodeId: 3, toNodeId: 1);

        Assert.False(result);
    }
}

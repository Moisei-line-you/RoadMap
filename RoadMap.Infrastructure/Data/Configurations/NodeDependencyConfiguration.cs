using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoadMap.Domain.Models.Roadmaps;

namespace RoadMap.Data.Configurations;

public class NodeDependencyConfiguration : IEntityTypeConfiguration<NodeDependency>
{
    public void Configure(EntityTypeBuilder<NodeDependency> builder)
    {
        builder.HasKey(nd => new { nd.FromNodeId, nd.ToNodeId });

        builder.HasOne(nd => nd.FromNode)
            .WithMany(n => n.DependsOn)
            .HasForeignKey(nd => nd.FromNodeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(nd => nd.ToNode)
            .WithMany(n => n.RequiredFor)
            .HasForeignKey(nd => nd.ToNodeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoadMap.Domain.Models.Roadmaps;

namespace RoadMap.Data.Configurations;

public class NodeResourceConfiguration : IEntityTypeConfiguration<NodeResource>
{
    public void Configure(EntityTypeBuilder<NodeResource> builder)
    {
        builder.HasKey(nr => new { nr.NodeId, nr.ResourceId });

        builder.HasOne(nr => nr.Node)
            .WithMany(n => n.Resources)
            .HasForeignKey(nr => nr.NodeId);

        builder.HasOne(nr => nr.Resource)
            .WithMany()
            .HasForeignKey(nr => nr.ResourceId);
    }
}
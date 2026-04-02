using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoadMap.Domain.Models.Roadmaps;

namespace RoadMap.Data.Configurations;

public class RoadmapNodeConfiguration : IEntityTypeConfiguration<RoadmapNode>
{
    public void Configure(EntityTypeBuilder<RoadmapNode> builder)
    {
        builder.HasKey(rn => new { rn.RoadmapId, rn.NodeId });

        builder.HasOne(rn => rn.Roadmap)
            .WithMany(r => r.Nodes)
            .HasForeignKey(rn => rn.RoadmapId);

        builder.HasOne(rn => rn.Node)
            .WithMany(n => n.Roadmaps)
            .HasForeignKey(rn => rn.NodeId);
    }
}
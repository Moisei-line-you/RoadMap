using Microsoft.EntityFrameworkCore;
using RoadMap.Data.Configurations;
using RoadMap.Domain.Models.Roadmaps;
using RoadMap.Models.Social;
using RoadMap.Models.Users;

namespace RoadMap.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RoadmapNodeConfiguration).Assembly);
    }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }
    public DbSet<ActivityLog> ActivityLogs { get; set; }
    public DbSet<UserNodeProgress> UserNodeProgresses { get; set; }

    public DbSet<Comment> Comments { get; set; }
    public DbSet<Like> Likes { get; set; }
    
    public DbSet<Node> Nodes { get; set; }
    public DbSet<NodeDependency> NodeDependencies { get; set; }
    public DbSet<NodeResource> NodeResources { get; set; }
    public DbSet<Resource> Resources { get; set; }
    public DbSet<Roadmap> Roadmaps { get; set; }
    public DbSet<RoadmapNode> RoadmapNodes { get; set; }
}
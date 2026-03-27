using Microsoft.EntityFrameworkCore;
using RoadMap.Domain.Models.Roadmaps;
using RoadMap.Models.Social;
using RoadMap.Models.Users;

namespace RoadMap.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Roadmap> Roadmaps { get; set; } = null!;
    public DbSet<Node> Nodes { get; set; } = null!;
    public DbSet<Resource> Resources { get; set; } = null!;
    public DbSet<UserProgress> UserProgresses { get; set; } = null!;
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }
    public DbSet<ActivityLog> ActivityLogs { get; set; }

    public DbSet<Comment> Comments { get; set; }
    public DbSet<Like> Likes { get; set; }
}
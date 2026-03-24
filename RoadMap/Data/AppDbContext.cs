using Microsoft.EntityFrameworkCore;
using RoadMap.Models.Social;
using RoadMap.Models.Users;

namespace RoadMap.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }
    public DbSet<ActivityLog> ActivityLogs { get; set; }

    public DbSet<Comment> Comments { get; set; }
    public DbSet<Like> Likes { get; set; }
}
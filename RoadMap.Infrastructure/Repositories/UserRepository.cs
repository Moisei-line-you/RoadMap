using Microsoft.EntityFrameworkCore;
using RoadMap.Data;
using RoadMap.Domain.Interfaces;
using RoadMap.Models.Users;

namespace RoadMap.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;
    
    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
        return await _context.Users.AnyAsync(u => u.Email == email);
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
    }
    
    public async Task AddUserAsync(User user)
    {
        await _context.Users.AddAsync(user);
    }
    
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }   
}
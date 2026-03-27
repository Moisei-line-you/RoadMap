using Microsoft.EntityFrameworkCore;
using RoadMap.Application.Interfaces;
using RoadMap.Data;
using RoadMap.Models.Users;

namespace RoadMap.Application.Repositories;

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
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }

    public void AddUsers(User user)
    {
        _context.Users.Add(user);
    }
    
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }   
}
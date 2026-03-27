using RoadMap.Models.Users;

namespace RoadMap.Domain.Interfaces;

public interface IUserRepository
{
    Task<bool> EmailExistsAsync(string email);
    
    Task<User?> GetByUsernameAsync(string username);
    
    Task AddUserAsync(User user);
    
    Task SaveChangesAsync();
}
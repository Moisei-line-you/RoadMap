using RoadMap.Models.Users;

namespace RoadMap.Application.Interfaces;

public interface IUserRepository
{
    Task<bool> EmailExistsAsync(string email);
    Task<User?> GetByUsernameAsync(string username);
    
    void AddUsers(User user);
    
    Task AddUserAsync(User user);
}
using RoadMap.Models.Users;

namespace RoadMap.Application.Interfaces;

public interface ITokenService
{
    string GenerateJwtToken(User user);
}
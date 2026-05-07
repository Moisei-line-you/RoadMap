using MediatR;
using Microsoft.EntityFrameworkCore;
using RoadMap.Application.DTOs.Roadmaps;
using RoadMap.Data;

namespace RoadMap.Application.Features.Roadmaps.Queries;

public record GetAllRoadmapsQuery()
    : IRequest<List<RoadmapDto>>;
    
public class GetAllRoadmapsQueryHandler
    : IRequestHandler<GetAllRoadmapsQuery, List<RoadmapDto>>
{
    private readonly AppDbContext _context;

    public GetAllRoadmapsQueryHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<RoadmapDto>> Handle(
        GetAllRoadmapsQuery request,
        CancellationToken cancellationToken
    )
    {
        return await _context.Roadmaps
            .Select(r => new RoadmapDto(
                r.Id,
                r.Title,
                r.Description,
                new List<RoadmapNodeDto>()
            ))
            .ToListAsync(cancellationToken);
    }

}
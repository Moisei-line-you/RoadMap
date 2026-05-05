using MediatR;
using RoadMap.Domain.Interfaces;

namespace RoadMap.Application.Features.Progress.Commands;

public record UnmarkNodeCompleteCommand(
    int UserId,
    int RoadmapId,
    int NodeId
) : IRequest<Unit>;

public class UnmarkNodeCompleteHandler : IRequestHandler<UnmarkNodeCompleteCommand, Unit>
{
    private readonly IRepository _repository;
    
    public UnmarkNodeCompleteHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(UnmarkNodeCompleteCommand request, CancellationToken cancellationToken)
    {
        var progress = await _repository.Progress.GetAsyncProgress(
            request.UserId,
            request.NodeId,
            request.RoadmapId);

        if (progress != null)
            _repository.Delete(progress);

        await _repository.SaveChangesAsync();
        
        return Unit.Value;
    }
}


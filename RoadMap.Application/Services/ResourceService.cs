using RoadMap.Application.DTOs.Roadmaps;
using RoadMap.Application.Interfaces;
using RoadMap.Domain.Exceptions;
using RoadMap.Domain.Interfaces;
using RoadMap.Domain.Models.Roadmaps;

namespace RoadMap.Application.Services;

public class ResourceService : IResourceService
{
    private readonly IRepository _repository;

    public ResourceService(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<int> CreateAsync(CreateResourceRequest request)
    {
        var resource = new Resource
        {
            Title = request.Title,
            Url = request.Url,
            Type = request.Type,
            IsFree = request.IsFree
        };

        await _repository.AddAsync(resource);
        await _repository.SaveChangesAsync();

        return resource.Id;
    }

    public async Task<ResourceDto> GetByIdAsync(int id)
    {
        var resource = await _repository.Resources.GetAsync(id)
                       ?? throw new NotFoundException("Resource", id);

        return new ResourceDto(
            resource.Id,
            resource.Title,
            resource.Url,
            resource.Type,
            resource.IsFree
        );
    }

    public async Task<IEnumerable<ResourceDto>> GetAllAsync()
    {
        var resources = await _repository.Resources.GetAllAsync();

        return resources.Select(r => new ResourceDto(
            r.Id,
            r.Title,
            r.Url,
            r.Type,
            r.IsFree
        ));
    }

    public async Task DeleteAsync(int id)
    {
        var resource = await _repository.Resources.GetAsync(id)
                       ?? throw new NotFoundException("Resource", id);

        _repository.Delete(resource);

        await _repository.SaveChangesAsync();
    }
}
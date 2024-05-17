using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IWalkRepository
    {
        Task<List<Walk>> GetAll(string? filterOn, string? filterQuery, string? sortBy, bool isAscending=true, int pageNumber = 0, int pageSize = 1000);
        Task<Walk?> GetById(Guid id);
        Task<Walk> Create(Walk region);
        Task<Walk?> Update(Guid id, Walk region);
        Task<Walk?> Delete(Guid id);
    }
}

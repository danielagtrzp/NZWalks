using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IWalkRepository
    {
        Task<List<Walk>> GetAll();
        Task<Walk?> GetById(Guid id);
        Task<Walk> Create(Walk region);
        Task<Walk?> Update(Guid id, Walk region);
        Task<Walk?> Delete(Guid id);
    }
}

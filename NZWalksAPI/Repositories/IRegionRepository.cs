using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Repositories
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetAll();
        Task<Region?> GetById(Guid id);
        Task<Region> Create(Region region);
        Task<Region?> Update(Guid id, Region region);
        Task<Region?> Delete(Guid id);
    }
}

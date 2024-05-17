using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext dbContext;

        public SQLRegionRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Region>> GetAll()
        {
            return await dbContext.Regions.ToListAsync();
        }
        public async Task<Region?> GetById(Guid id)
        {
            return await dbContext.Regions.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Region> Create(Region region)
        {
            await dbContext.Regions.AddAsync(region);
            await dbContext.SaveChangesAsync();

            return region;
        }
        public async Task<Region?> Update(Guid id, Region pRegion)
        {
            var region = await dbContext.Regions.FirstOrDefaultAsync(i => i.Id == id);
            if (region == null)
                return null;

            region.Code = pRegion.Code;
            region.Name = pRegion.Name;
            region.RegionImgUrl = pRegion.RegionImgUrl;

            await dbContext.SaveChangesAsync();

            return region;
        }
        public async Task<Region?> Delete(Guid id)
        {
            var region = await dbContext.Regions.FirstOrDefaultAsync(i => i.Id == id);
           
            if(region == null)
                return null;

            dbContext.Remove(region);
            await dbContext.SaveChangesAsync();

            return region;

        }


    }
}

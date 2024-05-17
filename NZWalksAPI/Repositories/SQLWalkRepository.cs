using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly NZWalksDbContext dbContext;

        public SQLWalkRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Walk>> GetAll()
        {
            return await dbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();
        }
        public async Task<Walk?> GetById(Guid id)
        {
            return await dbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Walk> Create(Walk walk)
        {
            await dbContext.Walks.AddAsync(walk);
            await dbContext.SaveChangesAsync();

            return walk;
        }
        public async Task<Walk?> Update(Guid id, Walk pWalk)
        {
            var walk = await dbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(i => i.Id == id);
            if (walk == null)
                return null;

            walk.Name = pWalk.Name;
            walk.Description= pWalk.Description;
            walk.LengthInKm = pWalk.LengthInKm;
            walk.WalkImageUrl = pWalk.WalkImageUrl;
            walk.RegionId = pWalk.RegionId;
            walk.DifficultyId = pWalk.DifficultyId;

            await dbContext.SaveChangesAsync();

            return walk;
        }
        public async Task<Walk?> Delete(Guid id)
        {
            var walk = await dbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(i => i.Id == id);
           
            if(walk == null)
                return null;

            dbContext.Remove(walk);
            await dbContext.SaveChangesAsync();

            return walk;

        }


    }
}

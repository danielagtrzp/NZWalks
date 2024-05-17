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

        public async Task<List<Walk>> GetAll(string? filterOn, string? filterQuery, string? sortBy, bool isAscending)
        {
            //return await dbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();
            //1. make the result querable so I can sot and page it
            //2. split await dbContext.Walks.Include("Difficulty").Include("Region").ToListAsync(); in two statements so y can filter on

            //FILTERING
            var walks = dbContext.Walks.Include("Difficulty").Include("Region").AsQueryable();

            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x=>x.Name.Contains(filterQuery));
                }
            }

            //SORTING
            if (!string.IsNullOrWhiteSpace(sortBy) )
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x=>x.Name) : walks.OrderByDescending(x=>x.Name);
                }
                else if (sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.LengthInKm) : walks.OrderByDescending(x => x.LengthInKm);

                }
            }


            return await walks.ToListAsync();
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

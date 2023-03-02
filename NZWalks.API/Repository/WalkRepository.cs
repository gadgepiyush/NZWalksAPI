using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repository
{
    public class WalkRepository : IWalkRepository
    {
        private readonly NZWalksDbContext nZWalksDbContext;

        public WalkRepository(NZWalksDbContext nZWalksDbContext)
        {
            this.nZWalksDbContext = nZWalksDbContext;
        }

        public async Task<IEnumerable<Walk>> GetAllAsync()
        {
            return await nZWalksDbContext.Walks.Include(x=> x.Region).Include(x => x.WalkDiffuculty).ToListAsync();
        }

        public async Task<Walk> GetAsync(Guid Id)
        {
            return await nZWalksDbContext.Walks.Include(x => x.Region).Include(x => x.WalkDiffuculty)
                .FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<Walk> AddWalkAsync(Walk walk)
        {
            walk.Id = Guid.NewGuid();
            await nZWalksDbContext.AddAsync(walk); 
            await nZWalksDbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk> UpdateWalkAsync(Guid id, Walk walk)
        {
            var exisitingWalk = await  nZWalksDbContext.Walks.FindAsync(id);

            if (exisitingWalk == null)
                return null;

            exisitingWalk.Length = walk.Length;
            exisitingWalk.Name = walk.Name;
            exisitingWalk.WalkDiffuculty = walk.WalkDiffuculty;
            exisitingWalk.Region = walk.Region;

            await nZWalksDbContext.SaveChangesAsync();

            return exisitingWalk;
        }

        public async Task<Walk> DeleteWalkAsync(Guid id)
        {
            var walk = await nZWalksDbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);

            if (walk == null)
                return null;

            nZWalksDbContext.Walks.Remove(walk);
            await nZWalksDbContext.SaveChangesAsync();

            return walk;
        }
    }
}

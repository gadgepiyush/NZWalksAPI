using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repository
{
    public class WalkDifficultyRepository : IWalkDifficultyRepository
    {
        private readonly NZWalksDbContext nZWalksDbContext;

        public WalkDifficultyRepository(NZWalksDbContext nZWalksDbContext) 
        {
            this.nZWalksDbContext = nZWalksDbContext;
        }


        public async Task<IEnumerable<WalkDifficulty>> GetAllAsync()
        {
            return await nZWalksDbContext.WalksDiffuculty.ToListAsync();
        }
        

        public async Task<WalkDifficulty> GetAsync(Guid Id)
        {
            return await nZWalksDbContext.WalksDiffuculty.FirstOrDefaultAsync(x => x.Id == Id);
        }


        public async Task<WalkDifficulty> AddWalkAsync(WalkDifficulty walk)
        {
            walk.Id = Guid.NewGuid();

            await nZWalksDbContext.AddAsync(walk);
            await nZWalksDbContext.SaveChangesAsync();

            return walk;
        }


        public async Task<WalkDifficulty> UpdateWalkAsync(Guid id, WalkDifficulty walk)
        {
            var existingWalkDifficulty = await nZWalksDbContext.WalksDiffuculty.FindAsync(id);

            if (existingWalkDifficulty == null)
                return null;
            
            existingWalkDifficulty.Code = walk.Code;

            await nZWalksDbContext.SaveChangesAsync();

            return existingWalkDifficulty;
        }




        public async Task<WalkDifficulty> DeleteWalkAsync(Guid id)
        {
            var walkDifficulty = await nZWalksDbContext.WalksDiffuculty.FirstOrDefaultAsync(x => x.Id == id);

            if (walkDifficulty == null)
                return null;

            nZWalksDbContext.WalksDiffuculty.Remove(walkDifficulty);
            await nZWalksDbContext.SaveChangesAsync();

            return walkDifficulty;
        }

    }
}

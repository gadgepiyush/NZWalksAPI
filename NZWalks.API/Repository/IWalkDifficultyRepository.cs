using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repository
{
    public interface IWalkDifficultyRepository
    {
        Task<IEnumerable<WalkDifficulty>> GetAllAsync();

        Task<WalkDifficulty> GetAsync(Guid Id);

        Task<WalkDifficulty> AddWalkAsync(WalkDifficulty walk);

        Task<WalkDifficulty> UpdateWalkAsync(Guid id, WalkDifficulty walk);

        Task<WalkDifficulty> DeleteWalkAsync(Guid id);
    }
}

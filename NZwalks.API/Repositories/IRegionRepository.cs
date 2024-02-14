using NZwalks.API.Models.Domain;

namespace NZwalks.API.Repositories
{
    public interface IRegionRepository
    {
        Task<Region> createAsync(Region region);
        Task<List<Region>> GetAllAsync();
        Task<Region?> GetByIdAsync(Guid id);
        Task<Region?> UpdateAsync(Guid id, Region region);
        Task<Region?> DeleteAsync(Guid id);
    }
}

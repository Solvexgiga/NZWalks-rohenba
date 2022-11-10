using NZWalks_rohenba.API.Models.Domain;

namespace NZWalks_rohenba.API.Repositories
{
    public interface IRegionRepository
    {
        Task<IEnumerable<Region>>GetAllAsync();
    }
}

using Microsoft.EntityFrameworkCore;
using NZWalks_rohenba.API.data;
using NZWalks_rohenba.API.Models.Domain;

namespace NZWalks_rohenba.API.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext nZWalksContext;

        public RegionRepository(NZWalksDbContext nZWalksContext)
        {
            this.nZWalksContext = nZWalksContext;
        }

        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            return await nZWalksContext.Regions.ToListAsync();


        }
    }
}

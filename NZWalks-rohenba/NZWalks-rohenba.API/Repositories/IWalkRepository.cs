﻿using NZWalks_rohenba.API.Models.Domain;

namespace NZWalks_rohenba.API.Repositories
{
    public interface IWalkRepository
    {
        Task<IEnumerable<Walk>>GetAllAsync();
        Task<Walk>GetAsync(Guid id);
        Task<Walk>AddAsync(Walk walk);
        Task<Walk>UpdateAsync(Guid id, Walk walk);
        Task<Walk>DeleteAsync(Guid id);
    }
}

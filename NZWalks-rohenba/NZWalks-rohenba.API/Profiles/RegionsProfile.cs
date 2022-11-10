using AutoMapper;

namespace NZWalks_rohenba.API.Profiles
{
    public class RegionsProfile :Profile
    {
        public RegionsProfile()
        {
            CreateMap<Models.Domain.Region, Models.DTO.Region>()
                .ReverseMap();
        }
    }
}

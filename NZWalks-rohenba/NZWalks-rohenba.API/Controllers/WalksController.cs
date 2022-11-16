using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks_rohenba.API.Repositories;

namespace NZWalks_rohenba.API.Controllers
{
    [ApiController]
    [Route("controller")]
    public class WalksController : Controller
    {
        private readonly IWalkRepository walkRepsitory;
        private readonly IMapper mapper;

        public WalksController(IWalkRepository walkRepsitory, IMapper mapper)
        {
            this.walkRepsitory = walkRepsitory;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWalksAsync()
        {
            //fetch data from database - domain walks
            var walksDomain = await walkRepsitory.GetAllAsync();
            // convert domain walks to DTO walks
            var walksDTO = mapper.Map<List<Models.DTO.Walk>>(walksDomain);
            //Return response
            return Ok(walksDTO);
        }
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkAsync")]
        public async Task<IActionResult> GetWalkAsync(Guid id)
        {
            //Get walk Domain object from database
            var walkDomain = await walkRepsitory.GetAsync(id);

            //Convert Domain object to DTO
            var walkDTO = mapper.Map<Models.DTO.Walk>(walkDomain);

            //return response
            return Ok(walkDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddWalkAsync([FromBody] Models.DTO.AddWalkRequest addWalkRequest)
        {
            // Convert DTO to domain
            var walkDomain = new Models.Domain.Walk
            {
                Length = addWalkRequest.Length,
                Name = addWalkRequest.Name,
                RegionId = addWalkRequest.RegionId,
                WalkDifficultyId = addWalkRequest.WalkDifficultyId
            };

            //Pass domain object to Repository to persist this
            walkDomain = await walkRepsitory.AddAsync(walkDomain);

            //convert the domain object back to DTO
            var walkDTO = new Models.DTO.Walk
            {
                Id = walkDomain.Id,
                Length = walkDomain.Length,
                Name = walkDomain.Name,
                RegionId = walkDomain.RegionId,
                WalkDifficultyId = walkDomain.WalkDifficultyId
            };

            // send DTO response back to client
            return CreatedAtAction(nameof(GetWalkAsync), new { id = walkDTO.Id }, walkDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalkAsync([FromRoute] Guid id, [FromBody] Models.DTO.UpdateWalkRequest updateWalkRequest)
        {
            // Convert DTO to Domain object
            var walkDomain = new Models.Domain.Walk
            {
                Length = updateWalkRequest.Length,
                Name = updateWalkRequest.Name,
                RegionId = updateWalkRequest.RegionId,
                WalkDifficultyId = updateWalkRequest.WalkDifficultyId
            };

            //Pass details to Repository -Get Domain object in response (or null)
            walkDomain = await walkRepsitory.UpdateAsync(id, walkDomain);

            //Handle Null (not found)
            if (walkDomain == null)
            {
                return NotFound();
            }
            else
            {
                //Convert back Domain to DTO
                var walkDTO = new Models.DTO.Walk
                {
                    Id = walkDomain.Id,
                    Length = walkDomain.Length,
                    Name = walkDomain.Name,
                    RegionId = walkDomain.RegionId,
                    WalkDifficultyId = walkDomain.WalkDifficultyId
             
              };

                //Return Response
                return Ok(walkDTO);

            }
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWalkAsync(Guid id)
        {
            //call Repository to delete walk
            var walkDomain = await walkRepsitory.DeleteAsync(id);
            if (walkDomain == null)
            {
                return NotFound("Walks was not found");
            }
            var walkDTO = mapper.Map<Models.DTO.Walk>(walkDomain);
            return Ok(walkDTO);
        }
    }
}

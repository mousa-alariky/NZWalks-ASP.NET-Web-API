using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZwalks.API.CustomActionFilters;
using NZwalks.API.Models.Domain;
using NZwalks.API.Models.DTO;
using NZwalks.API.Repositories;

namespace NZwalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IWalkRepository walkRepository;
        private readonly IMapper mapper;

        public WalksController(IWalkRepository walkRepository, IMapper mapper)
        {
            this.walkRepository = walkRepository;
            this.mapper = mapper;
        }


        // Create a new Walk
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreateAsync([FromBody] AddWalksRequestDto addWalksRequestDto)
        {

            // map dto to walk domain 
            var walkDomain = mapper.Map<Walk>(addWalksRequestDto);

            // create a new walk
            await walkRepository.CreateAsync(walkDomain);

            //map walk domain to dto and return dto
            return Ok(mapper.Map<WalkDto>(walkDomain));

        }


        // get all Walks
        // /api/walks?filterOn=Name&filterQuery=Track&sortBy=Name&isAscending=true&pageNumber=1&pageSize=10
        [HttpGet]
        public async Task<IActionResult> GetAllAsync(
            [FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000
            )
        {
            var walkDomain = await walkRepository.GetAllAsync(filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize);

            //map walk domain to dto and return dto
            return Ok(mapper.Map<List<WalkDto>>(walkDomain));
        }


        // get a walk by an id
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
        {
            var walkDomain = await walkRepository.GetByIdAsync(id);

            if (walkDomain == null)
            {
                return NotFound();
            }


            //map walk domain to dto and return dto
            return Ok(mapper.Map<WalkDto>(walkDomain));

        }


        // update a walk
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> updateAsync([FromRoute] Guid id, UpdateWalkDto updateWalkDto)
        {
            // map dto to walk domain
            var walkDomain = mapper.Map<Walk>(updateWalkDto);

            // update walk
            walkDomain = await walkRepository.UpdateAsync(id, walkDomain);

            if (walkDomain == null)
            {
                return NotFound();
            }

            //map walk domain to dto and return dto
            return Ok(mapper.Map<WalkDto>(walkDomain));


        }


        // delete a walk
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            var walkDomain = await walkRepository.DeleteAsync(id);

            if (walkDomain == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<WalkDto>(walkDomain));
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            _regionRepository = regionRepository;
            _mapper = mapper;
        }

        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            // map domain models to DTOs
            var regionDomain = _mapper.Map<Region>(addRegionRequestDto);

            // create a Region
            regionDomain = await _regionRepository.CreateAsync(regionDomain);

            // map domain model back to DTO
            var regionsDto = _mapper.Map<RegionDto>(regionDomain);

            //return status 201 created  success...
            return CreatedAtAction(nameof(GetById), new { id = regionsDto.Id }, regionsDto);
        }


        [HttpGet]
        [Authorize(Roles = "Reader,Writer")]
        public async Task<IActionResult> GetAll()
        {
            // get Region from database (domain models)
            var regionDomain = await _regionRepository.GetAllAsync();

            // return DTOs
            return Ok(_mapper.Map<List<RegionDto>>(regionDomain));
        }


        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader,Writer")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            // get Region from database (domain models)
            var regionDomain = await _regionRepository.GetByIdAsync(id);

            if (regionDomain == null) return NotFound();

            // return DTOs
            return Ok(_mapper.Map<RegionDto>(regionDomain));
        }


        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionDto updateRegionDto)
        {
            // map dto to domain model
            var regionDomain = _mapper.Map<Region>(updateRegionDto);

            // check if Region exists..
            regionDomain = await _regionRepository.UpdateAsync(id, regionDomain);

            if (regionDomain == null) return NotFound();

            // map domain model to dto and return dto
            return Ok(_mapper.Map<RegionDto>(regionDomain));
        }


        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionDomain = await _regionRepository.DeleteAsync(id);

            if (regionDomain == null) return NotFound();

            // map domain model to dto and return dto
            return Ok(_mapper.Map<RegionDto>(regionDomain));
        }
    }
}

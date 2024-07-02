using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext _dbContext;
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;

        public RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepository, IMapper mapper)
        {
            this._dbContext = dbContext;
            this._regionRepository = regionRepository;
            this._mapper = mapper;
        }

        // create a new Region
        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {

            // map domain models to DTOs
            var regionDomain = _mapper.Map<Region>(addRegionRequestDto);

            // create a Region
            regionDomain = await _regionRepository.createAsync(regionDomain);

            // map domain model back to DTO
            var regionsDto = _mapper.Map<RegionDto>(regionDomain);

            //return status 201 created  success...
            return CreatedAtAction(nameof(GetById), new { id = regionsDto.Id }, regionsDto);
        }


        // get all regions
        [HttpGet]
        [Authorize(Roles = "Reader,Writer")]
        public async Task<IActionResult> GetAll()
        {
            // get Region from database (domain models)
            var regionDomain = await _regionRepository.GetAllAsync();

            // map domain models to DTOs
            //var regionsDto = new List<RegionDto>();
            //foreach (var regionDomain in regionsDomain)
            //{
            //    regionsDto.Add(new RegionDto()
            //    {
            //        Id = regionDomain.Id,
            //        Code = regionDomain.Code,
            //        Name = regionDomain.Name,
            //        RegionImageUrl = regionDomain.RegionImageUrl,
            //    });
            //}


            // return DTOs
            return Ok(_mapper.Map<List<RegionDto>>(regionDomain));
        }


        // get a single region by id
        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader,Writer")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //var region = dbContext.Regions.Find(id); // can only be used with id

            // get Region from database (domain models)
            var regionDomain = await _regionRepository.GetByIdAsync(id);


            if (regionDomain == null)
            {
                return NotFound();
            }

            // map domain models to DTOs
            //var regionDto = new RegionDto
            //{
            //    Id = regionDomain.Id,
            //    Code = regionDomain.Code,
            //    Name = regionDomain.Name,
            //    RegionImageUrl = regionDomain.RegionImageUrl,
            //};



            // return DTOs
            return Ok(_mapper.Map<RegionDto>(regionDomain));

        }


        // update a Region
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
            if (regionDomain == null)
            {
                return NotFound();
            }

            // map domain model to dto and return dto
            return Ok(_mapper.Map<RegionDto>(regionDomain));


        }


        // delete a Region
        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionDomain = await _regionRepository.DeleteAsync(id);

            if (regionDomain == null)
            {
                return NotFound();
            }

            // map domain model to dto and return dto
            return Ok(_mapper.Map<RegionDto>(regionDomain));

        }
    }
}

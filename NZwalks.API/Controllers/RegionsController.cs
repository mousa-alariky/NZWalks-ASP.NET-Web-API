using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NZwalks.API.CustomActionFilters;
using NZwalks.API.Data;
using NZwalks.API.Models.Domain;
using NZwalks.API.Models.DTO;
using NZwalks.API.Repositories;

namespace NZalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepository, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        // create a new Region
        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {

            // map domain models to DTOs
            var regionDomain = mapper.Map<Region>(addRegionRequestDto);

            // create a Region
            regionDomain = await regionRepository.createAsync(regionDomain);

            // map domain model back to DTO
            var regionsDto = mapper.Map<RegionDto>(regionDomain);

            //return status 201 created  success...
            return CreatedAtAction(nameof(GetById), new { id = regionsDto.Id }, regionsDto);

        }


        // get all regions
        [HttpGet]
        [Authorize(Roles = "Reader,Writer")]
        public async Task<IActionResult> GetAll()
        {
            // get Region from database (domain models)
            var regionDomain = await regionRepository.GetAllAsync();

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
            return Ok(mapper.Map<List<RegionDto>>(regionDomain));
        }


        // get a single region by id
        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader,Writer")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //var region = dbContext.Regions.Find(id); // can only be used with id

            // get Region from database (domain models)
            var regionDomain = await regionRepository.GetByIdAsync(id);


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
            return Ok(mapper.Map<RegionDto>(regionDomain));

        }


        // update a Region
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionDto updateRegionDto)
        {

            // map dto to domain model
            var regionDomain = mapper.Map<Region>(updateRegionDto);

            // check if Region exists..
            regionDomain = await regionRepository.UpdateAsync(id, regionDomain);
            if (regionDomain == null)
            {
                return NotFound();
            }

            // map domain model to dto and return dto
            return Ok(mapper.Map<RegionDto>(regionDomain));


        }


        // delete a Region
        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionDomain = await regionRepository.DeleteAsync(id);

            if (regionDomain == null)
            {
                return NotFound();
            }

            // map domain model to dto and return dto
            return Ok(mapper.Map<RegionDto>(regionDomain));

        }
    }
}

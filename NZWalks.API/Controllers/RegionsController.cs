﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using NZWalks.API.Repositories.Interface;

namespace NZWalks.API.Controllers
{
    // https://localhost:1234/api/regions

    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepository,
             IMapper mapper)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }



        //// GET ALL REGIONS
        //// GET: https://localhost:portnumber/api/regions
        //// https://localhost:1234/api/regions
        //[HttpGet]
        //public IActionResult GetAll()
        //{



        //    // Get Data From Database - Domain models
        //    var regionsDomain = dbContext.Regions.ToList();

        //    // Map Domain Models to DTOs
        //    var regionsDto = new List<RegionDto>();
        //    foreach (var regionDomain in regionsDomain)
        //    {
        //        regionsDto.Add(new RegionDto()
        //        {
        //            Id = regionDomain.Id,
        //            Code = regionDomain.Code,
        //            Name = regionDomain.Name,
        //            RegionImageUrl = regionDomain.RegionImageUrl
        //        });
        //    }

        //    // Return DTOs
        //    return Ok(regionsDto);


        //    // Get Data From Database - Domain models
        //    //var regions = dbContext.Regions.ToList();



        //    //var regions = new List<Region>
        //    //{
        //    //    new Region
        //    //    {
        //    //        Id = Guid.NewGuid(),
        //    //        Name = "n",
        //    //        RegionImageUrl="4drdrtf.jpg"

        //    //    },
        //    //    new Region
        //    //    {
        //    //        Id = Guid.NewGuid(),
        //    //        Name = "n",

        //    //    }
        //    //};

        //    //return Ok(regions);

        //}


        // GET ALL REGIONS
        // GET: https://localhost:portnumber/api/regions
        // https://localhost:1234/api/regions
        [HttpGet]
        [Authorize(Roles = "Writer,Reader")]
        public async Task<IActionResult> GetAll()
        {



            // Get Data From Database - Domain models
            //var regionsDomain = await dbContext.Regions.ToListAsync();
            var regionsDomain = await regionRepository.GetAllAsync();

            // Map Domain Models to DTOs
            //var regionsDto = new List<RegionDto>();
            // foreach (var regionDomain in regionsDomain)
            // {
            //     regionsDto.Add(new RegionDto()
            //     {
            //         Id = regionDomain.Id,
            //         Code = regionDomain.Code,
            //         Name = regionDomain.Name,
            //         RegionImageUrl = regionDomain.RegionImageUrl
            //     });
            // }



            // using auto mapper
            var regionsDto = mapper.Map<List<RegionDto>>(regionsDomain);

            // Return DTOs
            return Ok(regionsDto);


            //return Ok(mapper.Map<List<RegionDto>>(regionsDomain));



        }








        // GET SINGLE REGION (Get Region By ID)
        // GET: https://localhost:portnumber/api/regions/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader")]

        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //var region = dbContext.Regions.Find(id);
            // Get Region Domain Model From Database
            //var regionDomain = dbContext.Regions.FirstOrDefault(x => x.Id == id);
            // var regionDomain =await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            var regionDomain = await regionRepository.GetByIdAsync(id);

            if (regionDomain == null)
            {
                return NotFound();
            }

            //// Map/Convert Region Domain Model to Region DTO

            //var regionDto = new RegionDto
            //{
            //    Id = regionDomain.Id,
            //    Code = regionDomain.Code,
            //    Name = regionDomain.Name,
            //    RegionImageUrl = regionDomain.RegionImageUrl
            //};

            //// Return DTO back to client
            //return Ok(regionDto);

            // Return DTO back to client
            return Ok(mapper.Map<RegionDto>(regionDomain));






            //var region = dbContext.Regions.Find(id);

            // Get Region Domain Model From Database
            //var region = dbContext.Regions.FirstOrDefault(x => x.Id == id);

            //if (region == null)
            //{
            //    return NotFound();
            //}


            //return Ok(region);
        }











        // POST To Create New Region
        // POST: https://localhost:portnumber/api/regions
        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {


                // Map or Convert DTO to Domain Model
                //var regionDomainModel = new Region
                //{
                //    Code = addRegionRequestDto.Code,
                //    Name = addRegionRequestDto.Name,
                //    RegionImageUrl = addRegionRequestDto.RegionImageUrl
                //};

                // Map or Convert DTO to Domain Model
                // destinatinon is Region
                // source is addRegionRequestDto
                // addRegionRequestDto it is comming
                var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);


                // Use Domain Model to create Region
                //dbContext.Regions.Add(regionDomainModel);
                //dbContext.SaveChanges();
                //await dbContext.Regions.AddAsync(regionDomainModel);
                //await dbContext.SaveChangesAsync();
                regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);

                // Map Domain model back to DTO
                //var regionDto = new RegionDto
                //{
                //    Id = regionDomainModel.Id,
                //    Code = regionDomainModel.Code,
                //    Name = regionDomainModel.Name,
                //    RegionImageUrl = regionDomainModel.RegionImageUrl
                //};

                // Map Domain model back to DTO
                // RegionDto ta convert korbo from regionDomainModel
                var regionDto = mapper.Map<RegionDto>(regionDomainModel);

                return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);







        }








        // Update region
        // PUT: https://localhost:portnumber/api/regions/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            if(ModelState.IsValid)
            {
                // map DTO to Domain Model
                //var regionDomainModel = new Region
                //{
                //    Code = updateRegionRequestDto.Code,
                //    Name = updateRegionRequestDto.Name,
                //    RegionImageUrl = updateRegionRequestDto.RegionImageUrl
                //};

                // map DTO to Domain Model
                // Region == updateRegionRequestDto
                // updateRegionRequestDto a ja pabo ta sb Region Domain ta dibo
                var regionDomainModel = mapper.Map<Region>(updateRegionRequestDto);


                // Check if region exists
                // var regionDomainModel = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
                regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);



                if (regionDomainModel == null)
                {
                    return NotFound();
                }

                //// Map DTO to Domain model
                //regionDomainModel.Code = updateRegionRequestDto.Code;
                //regionDomainModel.Name = updateRegionRequestDto.Name;
                //regionDomainModel.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;

                //await dbContext.SaveChangesAsync();

                // Convert Domain Model to DTO
                //var regionDto = new RegionDto
                //{
                //    Id = regionDomainModel.Id,
                //    Code = regionDomainModel.Code,
                //    Name = regionDomainModel.Name,
                //    RegionImageUrl = regionDomainModel.RegionImageUrl
                //};

                // Convert Domain Model to DTO
                var regionDto = mapper.Map<RegionDto>(regionDomainModel);

                return Ok(regionDto);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }








        // Delete Region
        // DELETE: https://localhost:portnumber/api/regions/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            //var regionDomainModel = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            var regionDomainModel = await regionRepository.DeleteAsync(id);


            if (regionDomainModel == null)
            {
                return NotFound();
            }

            // Delete region
            //dbContext.Regions.Remove(regionDomainModel);
            //await dbContext.SaveChangesAsync();// delete kori save



            // return deleted Region back
            // map Domain Model to DTO
            //var regionDto = new RegionDto
            //{
            //    Id = regionDomainModel.Id,
            //    Code = regionDomainModel.Code,
            //    Name = regionDomainModel.Name,
            //    RegionImageUrl = regionDomainModel.RegionImageUrl
            //};


            // return deleted Region back
            // map Domain Model to DTO


            var regionDto = mapper.Map<RegionDto>(regionDomainModel);

            return Ok(regionDto);
        }



    }
}

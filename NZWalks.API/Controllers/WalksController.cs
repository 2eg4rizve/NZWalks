using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Entities.RequestEntity;
using NZWalks.API.Managers.Interface;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using NZWalks.API.Repositories.Interface;

namespace NZWalks.API.Controllers
{


    // /api/walks
    [Route("api/[controller]")]
    [ApiController]
   
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;
      

        //public WalksController(IMapper mapper, IWalkRepository walkRepository, IWalksManager walksManager)
        //{
        //    this.mapper = mapper;
        //    this.walkRepository = walkRepository;
        //    _walksManager = walksManager;
        //}

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
            
        }


        // CREATE Walk
        //// POST: /api/walks
        //[HttpPost]
        //[ValidateModel]
        //public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        //{
        //    // request entity logger
        //    // call manager
        //   // var res = await _walksManager.AddWalks(walksRequestAddRequestEntity);
        //    // logginh
        //    //return Ok(res);
        //    //if (ModelState.IsValid)
        //    //{
        //    // Map DTO to Domain Model
        //    //var walkDomainModel = mapper.Map<Walk>(addWalkRequestDto);

        //    //await walkRepository.CreateAsync(walkDomainModel);

        //    //// Map Domain model to DTO
        //    //return Ok(mapper.Map<WalkDto>(walkDomainModel));
        //    //}


        //    // Map DTO to Domain Model
        //    var walkDomainModel = mapper.Map<Walk>(addWalkRequestDto);

        //    await walkRepository.CreateAsync(walkDomainModel);

        //    return Ok(mapper.Map<WalkDto>(walkDomainModel));


        //}

        // CREATE Walk
        // POST: /api/walks
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            // Map DTO to Domain Model
            var walkDomainModel = mapper.Map<Walk>(addWalkRequestDto);

            await walkRepository.CreateAsync(walkDomainModel);

            // Map Domain model to DTO
            return Ok(mapper.Map<WalkDto>(walkDomainModel));
        }


        //// Get Walks
        //// GET: /api/walks
        //[HttpGet]
        //public async Task<IActionResult> GetAll()
        //{


        //    var walksDomainModel = await walkRepository.GetAllAsync();

        //    // Map Domain Model to DTO


        //    return Ok(mapper.Map<List<WalkDto>>(walksDomainModel));
        //}




        // GET Walks
        // GET: /api/walks?filterOn=Name&filterQuery=Track&sortBy=Name&isAscending=true&pageNumber=1&pageSize=10
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000)
        {
            var walksDomainModel = await walkRepository.GetAllAsync(filterOn, filterQuery, sortBy,
                isAscending ?? true, pageNumber, pageSize);

            // Map Domain Model to DTO
            return Ok(mapper.Map<List<WalkDto>>(walksDomainModel));
        }








        //Get walk By id
        //Get: /api/walks/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walksDomainMode = await walkRepository.GetByIdAsync(id);

            if (walksDomainMode == null)
            {
                return NotFound();
            }

            //Map Domain Model to DTO
            return Ok(mapper.Map<WalkDto>(walksDomainMode));
        }



        // Update Walk By Id
        // PUT: /api/Walks/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, UpdateWalkRequestDto updateWalkRequestDto)
        {

            // Map DTO to Domain Model
            var walkDomainModel = mapper.Map<Walk>(updateWalkRequestDto);

            walkDomainModel = await walkRepository.UpdateAsync(id, walkDomainModel);

            if (walkDomainModel == null)
            {
                return NotFound();
            }

            // Map Domain Model to DTO
            return Ok(mapper.Map<WalkDto>(walkDomainModel));


        }



        // Delete a Walk By Id
        // DELETE: /api/Walks/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deletedWalkDomainModel = await walkRepository.DeleteAsync(id);

            if (deletedWalkDomainModel == null)
            {
                return NotFound();
            }

            // Map Domain Model to DTO
            return Ok(mapper.Map<WalkDto>(deletedWalkDomainModel));
        }







    }
}


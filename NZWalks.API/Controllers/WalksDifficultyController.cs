using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Repository;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("WalksDifficulty")]
    public class WalksDifficultyController : Controller
    {
        private readonly IWalkDifficultyRepository walkDifficultyRepository;
        private readonly IMapper mapper;

        public WalksDifficultyController(IWalkDifficultyRepository walkDifficultyRepository, IMapper mapper)
        {
            this.walkDifficultyRepository = walkDifficultyRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var walksDifficultyDomain = await walkDifficultyRepository.GetAllAsync();

            var walksDifficultyDTO = mapper.Map<List<Models.DTO.WalkDifficulty>>(walksDifficultyDomain);

            return Ok(walksDifficultyDTO);
        }


        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetById")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var walkDifficulty = await walkDifficultyRepository.GetAsync(id);

            if(walkDifficulty == null)
                return NotFound();

            var walkDifficultyDTO = mapper.Map<Models.DTO.WalkDifficulty>(walkDifficulty);

            return Ok(walkDifficultyDTO);
        }


        [HttpPost]
        public async Task<IActionResult> AddWalkDifficulty([FromBody] Models.DTO.AddWalkDifficultyRequest walkDifficulty)
        {
            //Covert DTO to Domain Object
            var addWalkDifficulty = new Models.Domain.WalkDifficulty { 
                Code = walkDifficulty.Code,
            };

            //Pass Domain Object to the repository to persist data
            addWalkDifficulty = await walkDifficultyRepository.AddWalkAsync(addWalkDifficulty);

            //convert domain object back to a DTO
            var addWalkDifficultyDTO = mapper.Map<Models.DTO.WalkDifficulty>(addWalkDifficulty);

            return CreatedAtAction(nameof(GetById), new {id = addWalkDifficultyDTO.Id}, addWalkDifficultyDTO);
        }


        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalks([FromRoute] Guid id, [FromBody] Models.DTO.UpdateWalksDifficultyRequest walksDifficultyRequest)
        {
            //Convert DTO to Domain
            var updateWalkDifficulty = new Models.Domain.WalkDifficulty
            {
                Code = walksDifficultyRequest.Code,
            };

            //Call the function in the repository
            updateWalkDifficulty = await walkDifficultyRepository.UpdateWalkAsync(id, updateWalkDifficulty);

            //convert Domain to DTO
            var updateWalkDifficultyDTO = mapper.Map<Models.DTO.WalkDifficulty>(updateWalkDifficulty);

            return Ok(updateWalkDifficultyDTO);
        }


        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWalkDifficulty(Guid id)
        {
            var walkDifficulty = await walkDifficultyRepository.DeleteWalkAsync(id);

            if(walkDifficulty == null)
                return NotFound();

            return Ok(walkDifficulty);
        }
    }
}

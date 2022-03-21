using AutoMapper;
using CosmosDbCrud.Dto;
using CosmosDbCrud.Models;
using CosmosDbCrud.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CosmosDbCrud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly ICosmosDbRepository<Item> _cosmosDbRepository;
        private readonly IMapper _mapper;

        public ItemsController(ICosmosDbRepository<Item> cosmosDbRepository, IMapper mapper)
        {
            _cosmosDbRepository = cosmosDbRepository ?? throw new ArgumentNullException(nameof(cosmosDbRepository));
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ItemToReturnDto>>> List()
        {
            var data = await _cosmosDbRepository.GetAllAsync();
            return Ok(_mapper.Map<List<ItemToReturnDto>>(data));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemToReturnDto>> Get(string id)
        {
            var data = await _cosmosDbRepository.GetAsync(id);
            return Ok(_mapper.Map<ItemToReturnDto>(data));
        }

        [HttpPost]
        public async Task<ActionResult<ItemToReturnDto>> Create([FromBody] ItemToCreateDto item)
        {
            var data = await _cosmosDbRepository.AddAsync(_mapper.Map<Item>(item));
            return CreatedAtAction(nameof(Get), new { id = data.Id }, _mapper.Map<ItemToReturnDto>(data));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ItemToReturnDto>> Edit([FromBody] ItemToUpdateDto item)
        {
            var data = await _cosmosDbRepository.AddAsync(_mapper.Map<Item>(item));
            return Ok(data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _cosmosDbRepository.DeleteAsync(id);
            return Ok();
        }
    }
}

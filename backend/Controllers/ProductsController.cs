using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.API.DTOs;
using ProductCatalog.API.Entities;
using ProductCatalog.API.Repositories;

namespace ProductCatalog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepo;
        private readonly ICategoryRepository _categoryRepo;
        private readonly IMapper _mapper;

        public ProductsController(IProductRepository productRepo, ICategoryRepository categoryRepo, IMapper mapper)
        {
            _productRepo = productRepo;
            _categoryRepo = categoryRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int? categoryId, [FromQuery] string? sortBy, [FromQuery] bool asc = true, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var products = await _productRepo.GetAllWithCategoryAsync(categoryId, sortBy, asc, page, pageSize);
            var dto = _mapper.Map<IEnumerable<ProductDto>>(products);
            return Ok(dto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var product = await _productRepo.GetWithCategoryByIdAsync(id);
            if (product == null) return NotFound();
            return Ok(_mapper.Map<ProductDto>(product));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductCreateDto createDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var category = await _categoryRepo.GetByIdAsync(createDto.CategoryId);
            if (category == null) return BadRequest($"Category with id {createDto.CategoryId} not found.");

            var product = _mapper.Map<Product>(createDto);
            product.CreatedDate = DateTime.UtcNow;

            var created = await _productRepo.AddAsync(product);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, _mapper.Map<ProductDto>(created));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductUpdateDto updateDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (id != updateDto.Id) return BadRequest("Id mismatch");

            var existing = await _productRepo.GetByIdAsync(id);
            if (existing == null) return NotFound();

            var category = await _categoryRepo.GetByIdAsync(updateDto.CategoryId);
            if (category == null) return BadRequest($"Category with id {updateDto.CategoryId} not found.");

            _mapper.Map(updateDto, existing);
            await _productRepo.UpdateAsync(existing);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _productRepo.GetByIdAsync(id);
            if (existing == null) return NotFound();

            await _productRepo.DeleteAsync(existing);
            return NoContent();
        }
    }
}

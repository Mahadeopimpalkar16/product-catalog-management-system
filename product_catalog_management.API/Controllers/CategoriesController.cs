using Microsoft.AspNetCore.Mvc;
using ProductCatalog.API.DTOs;
using ProductCatalog.API.Services;

namespace ProductCatalog.API.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoriesController(ICategoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _service.GetAllCategoriesAsync();
            return Ok(categories);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateDto dto)
        {
            var created = await _service.CreateCategoryAsync(dto);
            return CreatedAtAction(nameof(GetAll), created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CategoryUpdateDto dto)
        {
            await _service.UpdateCategoryAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteCategoryAsync(id);
            return NoContent();
        }
    }
}

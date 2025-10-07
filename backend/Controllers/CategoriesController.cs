using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.API.DTOs;
using ProductCatalog.API.Entities;
using ProductCatalog.API.Repositories;

namespace ProductCatalog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepo;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryRepository categoryRepo, IMapper mapper)
        {
            _categoryRepo = categoryRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var cats = await _categoryRepo.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<CategoryDto>>(cats));
        }
    }
}

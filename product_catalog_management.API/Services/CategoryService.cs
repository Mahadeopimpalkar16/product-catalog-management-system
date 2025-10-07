using AutoMapper;
using ProductCatalog.API.DTOs;
using ProductCatalog.API.Entities;
using ProductCatalog.API.Repositories;

namespace ProductCatalog.API.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repo;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<CategoryDto> CreateCategoryAsync(CategoryCreateDto dto)
        {
            var category = _mapper.Map<Category>(dto);
            var created = await _repo.AddAsync(category);
            return _mapper.Map<CategoryDto>(created);
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await _repo.GetByIdAsync(id) ?? throw new KeyNotFoundException("Category not found");
            await _repo.DeleteAsync(category);
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
        {
            var categories = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }

        public async Task UpdateCategoryAsync(int id, CategoryUpdateDto dto)
        {
            var category = await _repo.GetByIdAsync(id) ?? throw new KeyNotFoundException("Category not found");
            _mapper.Map(dto, category);
            await _repo.UpdateAsync(category);
        }
    }
}

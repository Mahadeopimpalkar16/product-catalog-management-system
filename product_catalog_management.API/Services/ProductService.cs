using AutoMapper;
using ProductCatalog.API.DTOs;
using ProductCatalog.API.Entities;
using ProductCatalog.API.Repositories;

namespace ProductCatalog.API.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<ProductDto> CreateProductAsync(ProductCreateDto dto)
        {
            var product = _mapper.Map<Product>(dto);
            product.CreatedDate = DateTime.UtcNow;
            var created = await _repo.AddAsync(product);
            return _mapper.Map<ProductDto>(await _repo.GetWithCategoryByIdAsync(created.Id)!);
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await _repo.GetByIdAsync(id) ?? throw new KeyNotFoundException("Product not found");
            await _repo.DeleteAsync(product);
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync(int? categoryId = null, string? sortBy = null, bool asc = true)
        {
            var products = await _repo.GetAllWithCategoryAsync(categoryId, sortBy, asc);
            //var result =  _mapper.Map<IEnumerable<ProductDto>>(products);
            var result = products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                CategoryName = p.Category.Name,
                CreatedDate = DateOnly.FromDateTime(p.CreatedDate)
            });
            return result;
        }

        public async Task<ProductDto?> GetProductByIdAsync(int id)
        {
            var product = await _repo.GetWithCategoryByIdAsync(id);
            return product == null ? null : _mapper.Map<ProductDto>(product);
        }

        public async Task UpdateProductAsync(ProductUpdateDto dto)
        {
            var product = await _repo.GetByIdAsync(dto.Id) ?? throw new KeyNotFoundException("Product not found");
            _mapper.Map(dto, product);
            await _repo.UpdateAsync(product);
        }
    }
}

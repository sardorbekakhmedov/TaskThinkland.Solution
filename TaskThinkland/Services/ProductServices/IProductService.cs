using TaskThinkland.Api.DTOs.ProductDTOs;
using TaskThinkland.Api.Filters;

namespace TaskThinkland.Api.Services.ProductServices;

public interface IProductService
{
    ValueTask<ProductDto> InsertAsync(CreateProductDto dto);
    ValueTask<IEnumerable<ProductDto>> GetAllAsync(ProductFilter filter);
    ValueTask<ProductDto> GetByIdAsync(Guid productId);
    ValueTask<ProductDto> UpdateAsync(Guid productId, UpdateProductDto dto);
    ValueTask DeleteAsync(Guid productId);
}
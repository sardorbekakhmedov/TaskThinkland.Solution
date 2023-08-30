using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaskThinkland.Api.Context.Repositories.GenericRepositories;
using TaskThinkland.Api.DTOs.ProductDTOs;
using TaskThinkland.Api.Entities;
using TaskThinkland.Api.Exceptions;
using TaskThinkland.Api.Extensions;
using TaskThinkland.Api.Filters;
using TaskThinkland.Api.PaginationModels;
using TaskThinkland.Api.Services.FileServices;

namespace TaskThinkland.Api.Services.ProductServices;

public class ProductService : IProductService
{
    private const string ProductImages = "ProductImages";

    private readonly IGenericRepository<Product> _productRepository;
    private readonly HttpContextHelper _httpContext;
    private readonly IFileService _fileService;
    private readonly IMapper _mapper;

    public ProductService(IGenericRepository<Product> productRepository, HttpContextHelper httpContext,
        IFileService fileService, IMapper mapper)
    {
        _productRepository = productRepository;
        _httpContext = httpContext;
        _fileService = fileService;
        _mapper = mapper;
    }

    public async ValueTask<ProductDto> InsertAsync(CreateProductDto dto)
    {
        var product = new Product()
        {
            Title = dto.Title,
            Description = dto.Description,
            Price = dto.Price,
            ImagePath = await _fileService.SaveFileAsync(dto.ImageFile, ProductImages)
        };

        var newProduct = await _productRepository.InsertAsync(product);

        return _mapper.Map<ProductDto>(newProduct);
    }

    public async ValueTask<IEnumerable<ProductDto>> GetAllAsync(ProductFilter filter)
    {

        var query = _productRepository.SelectAll().AsQueryable();

        if (filter.Title is not null)
            query = query.Where(p => p.Title.ToLower().Equals(filter.Title.ToLower()));

        if (filter.FromPrice is not null)
            query = query.Where(p => p.Price >= filter.FromPrice);

        if (filter.ToPrice is not null)
            query = query.Where(p => p.Price <= filter.ToPrice);

        if (filter.FromDateTime is not null)
            query = query.Where(p => p.CreateAt >= filter.FromDateTime);

        if (filter.ToDateTime is not null)
            query = query.Where(p => p.CreateAt <= filter.ToDateTime);

        var products = await query.AsNoTracking().ToPagedListAsync(_httpContext, filter);

        return products.Select(p => _mapper.Map<ProductDto>(p));
    }

    public async ValueTask<ProductDto> GetByIdAsync(Guid productId)
    {
        var product = await _productRepository.SelectSingleAsync(p => p.Id.Equals(productId));

        if (product is null)
            throw new NotFoundException($"{typeof(Product)} not found!");

        return _mapper.Map<ProductDto>(product);
    }

    public async ValueTask<ProductDto> UpdateAsync(Guid productId, UpdateProductDto dto)
    {
        var product = await _productRepository.SelectSingleAsync(p => p.Id.Equals(productId));

        if (product is null)
            throw new NotFoundException($"{typeof(Product)} not found!");

        product.Title = dto.Title ?? product.Title;
        product.Description = dto.Description ?? product.Description;
        product.Price = dto.Price ?? product.Price;

        if (dto.ImageFile is not null)
        {
            _fileService.DeleteFile(product.ImagePath);
            product.ImagePath = await _fileService.SaveFileAsync(dto.ImageFile, ProductImages);
        }

        var updatedProduct =  await _productRepository.UpdateAsync(product);

        return _mapper.Map<ProductDto>(updatedProduct);
    }

    public async ValueTask DeleteAsync(Guid productId)
    {
        var product = await _productRepository.SelectSingleAsync(p => p.Id.Equals(productId));

        if (product is null)
            throw new NotFoundException($"{typeof(Product)} not found!");

        _fileService.DeleteFile(product.ImagePath);

        await _productRepository.DeleteAsync(product);
    }
}
namespace TaskThinkland.Api.DTOs.ProductDTOs;

public class CreateProductDto
{
    public required IFormFile ImageFile { get; set; }
    public required string Title { get; set; }
    public required decimal Price { get; set; }
    public required string Description { get; set; }
}
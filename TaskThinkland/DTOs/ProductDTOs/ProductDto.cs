namespace TaskThinkland.Api.DTOs.ProductDTOs;

public class ProductDto
{
    public required Guid Id { get; set; }
    public required string Title { get; set; }
    public required decimal Price { get; set; }
    public required string Description { get; set; }
    public required string ImagePath { get; set; }
}
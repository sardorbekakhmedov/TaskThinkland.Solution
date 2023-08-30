namespace TaskThinkland.Api.Entities;

public class Product : BaseEntity
{
    public required string ImagePath { get; set; }
    public required string Title { get; set; }
    public required decimal Price { get; set;}
    public required string Description { get; set; } 
}
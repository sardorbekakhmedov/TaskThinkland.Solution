using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskThinkland.Api.DTOs.ProductDTOs;
using TaskThinkland.Api.Exceptions;
using TaskThinkland.Api.Filters;
using TaskThinkland.Api.Services.ProductServices;

namespace TaskThinkland.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Insert([FromForm] CreateProductDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            return Created("InsertProduct",await _productService.InsertAsync(dto));
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }


    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAll([FromQuery] ProductFilter filter)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            return Ok(await _productService.GetAllAsync(filter));
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }

    [HttpGet("{productId:guid}")]
    [Authorize]
    public async Task<IActionResult> GetById(Guid productId)
    {
        try
        {
            return Ok(await _productService.GetByIdAsync(productId));
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }

    [HttpPut("{productId:guid}")]
    [Authorize]
    public async Task<IActionResult> Update(Guid productId,[FromForm] UpdateProductDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            return Ok(await _productService.UpdateAsync(productId, dto));
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }

    [HttpDelete("{productId:guid}")]
    [Authorize]
    public async Task<IActionResult> Delete(Guid productId)
    {
        try
        {
            await _productService.DeleteAsync(productId);
            return Ok();
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }
}
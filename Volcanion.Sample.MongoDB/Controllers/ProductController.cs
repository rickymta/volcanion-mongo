using Microsoft.AspNetCore.Mvc;
using Volcanion.Sample.MongoDB.Infrastructure.Abstractions;
using Volcanion.Sample.MongoDB.Models.Documents;
using Volcanion.Sample.MongoDB.Models.Filtes;

namespace Volcanion.Sample.MongoDB.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IBaseRepository<ProductDocument> _repository;

    public ProductController(IBaseRepository<ProductDocument> repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _repository.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var product = await _repository.GetByIdAsync(id);
        return product == null ? NotFound() : Ok(product);
    }

    [HttpGet("paging")]
    public async Task<IActionResult> GetPaging([FromQuery] FilterBase filter)
    {
        var products = await _repository.GetPagingAsync(filter);
        return Ok(products);
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProductDocument product)
    {
        await _repository.CreateAsync(product);
        return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, ProductDocument product)
    {
        if (id != product.Id) return BadRequest();
        await _repository.UpdateAsync(product);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _repository.DeleteAsync(id);
        return NoContent();
    }
}

using FinaceTracker.Models;
using FinaceTracker.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinaceTracker.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PurchaseTypeController : ControllerBase
{
    private readonly PurchaseTypeService _purchaseTypeService;

    public PurchaseTypeController(PurchaseTypeService purchaseTypeService) =>
        _purchaseTypeService = purchaseTypeService;

    [HttpGet]
    public async Task<List<PurchaseType>> Get() =>
        await _purchaseTypeService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<PurchaseType>> Get(string id)
    {
        var purchaseType = await _purchaseTypeService.GetAsync(id);

        if (purchaseType is null)
        {
            return NotFound();
        }

        return purchaseType;
    }

    [HttpPost]
    public async Task<IActionResult> Post(PurchaseType newPurchaseType)
    {
        await _purchaseTypeService.CreateAsync(newPurchaseType);

        return CreatedAtAction(nameof(Get), new { id = newPurchaseType.Id }, newPurchaseType);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, PurchaseType updatedPurchaseType)
    {
        var purchaseType = await _purchaseTypeService.GetAsync(id);

        if (purchaseType is null)
        {
            return NotFound();
        }

        updatedPurchaseType.Id = purchaseType.Id;

        await _purchaseTypeService.UpdateAsync(id, updatedPurchaseType);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var purchase = await _purchaseTypeService.GetAsync(id);

        if (purchase is null)
        {
            return NotFound();
        }

        await _purchaseTypeService.RemoveAsync(id);

        return NoContent();
    }
}
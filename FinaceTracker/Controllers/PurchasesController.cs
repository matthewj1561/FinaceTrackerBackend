using FinaceTracker.Models;
using FinaceTracker.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinaceTracker.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PurchasesController : ControllerBase
{
    private readonly PurchasesService _purchasesService;

    public PurchasesController(PurchasesService purchasesService) =>
        _purchasesService = purchasesService;

    [HttpGet]
    public async Task<List<Purchase>> Get() =>
        await _purchasesService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Purchase>> Get(string id)
    {
        var purchase = await _purchasesService.GetAsync(id);

        if (purchase is null)
        {
            return NotFound();
        }

        return purchase;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Purchase newPurchase)
    {
        await _purchasesService.CreateAsync(newPurchase);

        return CreatedAtAction(nameof(Get), new { id = newPurchase.Id }, newPurchase);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Purchase updatedPurchase)
    {
        var purchase = await _purchasesService.GetAsync(id);

        if (purchase is null)
        {
            return NotFound();
        }

        updatedPurchase.Id = purchase.Id;

        await _purchasesService.UpdateAsync(id, updatedPurchase);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var purchase = await _purchasesService.GetAsync(id);

        if (purchase is null)
        {
            return NotFound();
        }

        await _purchasesService.RemoveAsync(id);

        return NoContent();
    }
}
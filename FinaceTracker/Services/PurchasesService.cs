using FinaceTracker.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace FinaceTracker.Services;

public class PurchasesService
{
    private readonly IMongoCollection<Purchase> _purchasesCollection;

    public PurchasesService(
        IOptions<PurchasesDatabaseSettings> PurchasesDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            PurchasesDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            PurchasesDatabaseSettings.Value.DatabaseName);

        _purchasesCollection = mongoDatabase.GetCollection<Purchase>(
            PurchasesDatabaseSettings.Value.PurchasesCollectionName);
    }

    public async Task<List<Purchase>> GetAsync() =>
        await _purchasesCollection.Find(_ => true).ToListAsync();

    public async Task<Purchase?> GetAsync(string id) =>
        await _purchasesCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Purchase newPurchase) =>
        await _purchasesCollection.InsertOneAsync(newPurchase);

    public async Task UpdateAsync(string id, Purchase updatedPurchase) =>
        await _purchasesCollection.ReplaceOneAsync(x => x.Id == id, updatedPurchase);

    public async Task RemoveAsync(string id) =>
        await _purchasesCollection.DeleteOneAsync(x => x.Id == id);
}
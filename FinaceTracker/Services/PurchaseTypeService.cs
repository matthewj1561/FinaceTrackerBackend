using FinaceTracker.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace FinaceTracker.Services;

public class PurchaseTypeService
{
    private readonly IMongoCollection<PurchaseType> _purchaseTypeCollection;

    public PurchaseTypeService(
        IOptions<PurchasesDatabaseSettings> PurchasesDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            PurchasesDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            PurchasesDatabaseSettings.Value.DatabaseName);

        _purchaseTypeCollection = mongoDatabase.GetCollection<PurchaseType>(
            PurchasesDatabaseSettings.Value.PurchaseTypesCollectionName);
    }

    public async Task<List<PurchaseType>> GetAsync() =>
        await _purchaseTypeCollection.Find(_ => true).ToListAsync();

    public async Task<PurchaseType?> GetAsync(string id) =>
        await _purchaseTypeCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(PurchaseType newPurchaseType) =>
        await _purchaseTypeCollection.InsertOneAsync(newPurchaseType);

    public async Task UpdateAsync(string id, PurchaseType updatedPurchaseType) =>
        await _purchaseTypeCollection.ReplaceOneAsync(x => x.Id == id, updatedPurchaseType);

    public async Task RemoveAsync(string id) =>
        await _purchaseTypeCollection.DeleteOneAsync(x => x.Id == id);
}
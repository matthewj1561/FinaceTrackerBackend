namespace FinaceTracker.Models
{
    public class PurchasesDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string PurchasesCollectionName { get; set; } = null!;

        public string PurchaseTypesCollectionName { get; set; } = null!;
    }
}

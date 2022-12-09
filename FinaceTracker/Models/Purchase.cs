using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FinaceTracker.Models
{
    public class Purchase
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string PurchasedBy { get; set; } = null!;

        public string Date { get; set; }

        public string PurchaseTypeId { get; set; }

        public decimal Amount { get; set; }

        public string Notes { get; set; } = null!;
    }
}

using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace FinaceTracker.Models
{
    public class PurchaseType
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        
        public string? TypeName { get; set; }   
    }
}

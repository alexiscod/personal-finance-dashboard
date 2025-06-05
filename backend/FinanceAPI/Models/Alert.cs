using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FinanceAPI.Models
{
    public class Alert
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;

        [BsonElement("UserId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; } = null!;

        [BsonElement("CategoryId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string CategoryId { get; set; } = null!;

        [BsonElement("TriggeredAt")]
        public DateTime TriggeredAt { get; set; } = DateTime.UtcNow;

        [BsonElement("Type")]
        public string Type { get; set; } = null!;

        [BsonElement("IsAcknowledged")]
        public bool IsAcknowledged { get; set; } = false;
    }
}

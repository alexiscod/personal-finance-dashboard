using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FinanceAPI.Models
{
    public class Transaction
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;

        [BsonElement("UserId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; } = null!;

        [BsonElement("Amount")]
        public decimal Amount { get; set; }

        [BsonElement("Date")]
        public DateTime Date { get; set; }

        [BsonElement("Description")]
        public string Description { get; set; } = null!;

        [BsonElement("Category")]
        public string Category { get; set; } = null!;

        [BsonElement("IsExpense")]
        public bool IsExpense { get; set; } = true;

        [BsonElement("CreatedAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}

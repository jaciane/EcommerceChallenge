using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.API.Entities
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }


        [BsonElement("id")]
        public int IdProduct { get; set; }

        [BsonElement("title")]
        public string ? Title { get; set; }

        [BsonElement("description")]
        public string ? Description { get; set; }
        
        [BsonElement("amount")]
        public decimal Amount { get; set; }

        [BsonElement("is_gift")]
        public bool Is_gift { get; set; }
    }
}

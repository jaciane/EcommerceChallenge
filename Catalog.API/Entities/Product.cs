using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.API.Entities
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }


        [BsonElement("id")]
        public string ? IdProduct { get; set; }

        [BsonElement("title")]
        public string ? Title { get; set; }

        [BsonElement("description")]
        public string ? Description { get; set; }
        
        [BsonElement("amount")]
        public string? Amount { get; set; }

        [BsonElement("is_gift")]
        public string ? Is_gift { get; set; }
    }
}

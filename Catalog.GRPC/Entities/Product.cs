using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Catalog.GRPC.Entities
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }


        [BsonElement("id")]
        [Display(Name = "id")]
        public int IdProduct { get; set; }

        [BsonElement("title")]
        [Display(Name = "title")]
        public string? Title { get; set; }

        [BsonElement("description")]
        [Display(Name = "description")]
        public string? Description { get; set; }

        [BsonElement("amount")]
        [Display(Name = "amount")]
        public double Amount { get; set; }

        [BsonElement("is_gift")]
        [Display(Name = "is_gift")]
        public bool Is_gift { get; set; }
    }
}

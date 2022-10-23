using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using Rise.Shared.Enums;

namespace Rise.MongoDb.Entity.Concreate;

public class Contact {

    [BsonElement("_id")]
    [JsonProperty("_id")]
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public ContactType ContactType { get; set; }
    public string ContactData { get; set; }

    public Contact()
    {
        Id = ObjectId.GenerateNewId().ToString();
    }
}
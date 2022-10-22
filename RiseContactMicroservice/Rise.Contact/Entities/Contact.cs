using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Newtonsoft.Json;
using Rise.Contact.DataAccess.Abstract;
using Rise.Shared.Enums;
using System.Security.Cryptography;

namespace Rise.Contact.Entities;

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
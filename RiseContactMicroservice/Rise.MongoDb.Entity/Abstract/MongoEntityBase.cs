﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Rise.MongoDb.Entity.Abstract;

public abstract class MongoEntityBase
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

}
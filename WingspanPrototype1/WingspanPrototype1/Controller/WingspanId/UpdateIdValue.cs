using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using WingspanPrototype1.Model;

namespace WingspanPrototype1.Controller.WingspanId
{
    class UpdateIdValue
    {
        public static bool UpdateIdDocument(string idType, int value)
        {
            var database = DatabaseConnection.GetDatabase();

            if (database != null)
            {
                IMongoCollection<BsonDocument> collection;
                switch (idType)
                {
                    case "WildIdValue":
                        collection = database.GetCollection<BsonDocument>("WildIdValue");
                        break;
                    case "CaptiveIdValue":
                        collection = database.GetCollection<BsonDocument>("CaptiveIdValue");
                        break;
                    default:
                        collection = null;
                        break;
                }

                if (collection != null)
                {
                    try
                    {
                        // Search db
                        var dbValue = collection.Find(new BsonDocument { }).First();

                        // Get Id Value 
                        var idValue = BsonSerializer.Deserialize<IdValue>(dbValue);

                        // Update Value
                        collection.UpdateOne(Builders<BsonDocument>.Filter.Eq("_id", idValue._id), Builders<BsonDocument>.Update.Set("Value", value));

                        return true;
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}

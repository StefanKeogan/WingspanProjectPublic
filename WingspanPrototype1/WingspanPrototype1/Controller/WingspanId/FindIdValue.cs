using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using WingspanPrototype1.Model;

namespace WingspanPrototype1.Controller.WingspanId
{
    class FindIdValue
    {
        public static IdValue GetWingspanIdValue(string idType)
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
                    // Search db
                    var dbValue = collection.Find(new BsonDocument { }).First();

                    // Get Id Value 
                    var idValue = BsonSerializer.Deserialize<IdValue>(dbValue);

                    return idValue;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }

            
        }
    }
}

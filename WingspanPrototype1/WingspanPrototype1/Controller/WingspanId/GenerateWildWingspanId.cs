using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using WingspanPrototype1.Model;

namespace WingspanPrototype1.Functions
{
    class GenerateWildWingspanId
    {
        public static string NewId()
        {
            // Get databse 
            var database = DatabaseConnection.GetDatabase();

            // Get collection
            var collection = database.GetCollection<BsonDocument>("WildIdValue");

            // Get value
            var value = collection.Find(new BsonDocument { }).First();

            // Get Id Value 
            var idValue = BsonSerializer.Deserialize<IdValue>(value);

            // Generate Wingspan ID
            int number = idValue.Value;
            int year = DateTime.Today.Year % 100;
            string wingspanId = "w" + year.ToString() + "/" + number.ToString();

            int increment = number + 1;

            // Increment Id Value 
            collection.UpdateOne(Builders<BsonDocument>.Filter.Eq("_id", idValue.Value_id), Builders<BsonDocument>.Update.Set("Value", increment));

            return wingspanId;
        }

    }
}

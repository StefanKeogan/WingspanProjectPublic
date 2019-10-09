using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using WingspanPrototype1.Model;

namespace WingspanPrototype1.Functions
{
    class GenerateWingspanId
    {
        public static string NewId()
        {
            var database = DatabaseConnection.GetDatabase();

            var collection = database.GetCollection<BsonDocument>("IdValue");

            var value = collection.Find(new BsonDocument { }).ToList();

            // Get Id Value 
            var idValue = BsonSerializer.Deserialize<IdValue>(value[0]);

            // Generate Wingspan ID
            int number = idValue.Value;
            int year = DateTime.Today.Year % 100;
            string wingspanId = year.ToString() + "/" + number.ToString();


            int increment = number+1;

            // Increment Id Value 
            collection.UpdateOne(Builders<BsonDocument>.Filter.Eq("_id", idValue._id), Builders<BsonDocument>.Update.Set("Value", increment));

            return wingspanId;
        }
    }
}

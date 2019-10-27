using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using WingspanPrototype1.Model;

namespace WingspanPrototype1.Controller.Birds
{
    class FindBirdLocationHistory
    {
        public static List<Location> GetLocationDocuments(string wingspanId)
        {
            // Get DB
            var database = DatabaseConnection.GetDatabase();

            if (database != null)
            {
                // Get note collection
                var collection = database.GetCollection<BsonDocument>("LocationHistory");

                try
                {
                    // Search note collection 
                    List<BsonDocument> locationResults = collection.Find(Builders<BsonDocument>.Filter.Eq("WingspanId", wingspanId)).ToList();

                    // Convert results to note results
                    List<Location> locationObjectResults = new List<Location>();
                    foreach (var result in locationResults)
                    {
                        locationObjectResults.Add(BsonSerializer.Deserialize<Location>(result));
                    }
                    return locationObjectResults;
                }
                catch (Exception)
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

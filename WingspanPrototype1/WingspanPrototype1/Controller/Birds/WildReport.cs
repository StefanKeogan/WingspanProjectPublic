using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using WingspanPrototype1.Functions;

namespace WingspanPrototype1.Controller.Birds
{
    class WildReport
    {       

        public static List<WildBird> BandedReport(DateTime startDate, DateTime endDate)
        {
            // Get database
            var database = DatabaseConnection.GetDatabase();

            // Get collection
            var collection = database.GetCollection<BsonDocument>("WildBirds");
            
            // Used to build reports 
            var filterBuilder = Builders<BsonDocument>.Filter;

            // Build master filter
            var filter = filterBuilder.Gte("DateBanded", startDate) & filterBuilder.Lte("DateBanded", endDate);

            try
            {
                // Search wild birds 
                var reportResults = collection.Find(filter).ToList();
                // Deserialize bson documents 
                List<WildBird> results = new List<WildBird>();
                foreach (var result in reportResults)
                {
                    results.Add(BsonSerializer.Deserialize<WildBird>(result));
                }
                return results;
            }
            catch (Exception)
            {
                return null;
            }                       
            
        }
        

    }

}

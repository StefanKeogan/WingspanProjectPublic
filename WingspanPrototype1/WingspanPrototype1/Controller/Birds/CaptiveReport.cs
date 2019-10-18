using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using WingspanPrototype1.Model;

namespace WingspanPrototype1.Controller.Birds
{
    class CaptiveReport
    {
        public static List<CaptiveBird> IngoingOutgoingReport(string condition, DateTime startDate, DateTime endDate)
        {
            // Get database
            var database = DatabaseConnection.GetDatabase();

            // Get collection
            var collection = database.GetCollection<BsonDocument>("CaptiveBirds");

            // Used to build reports 
            var filterBuilder = Builders<BsonDocument>.Filter;

            FilterDefinition<BsonDocument> filter = null;

            switch (condition.ToString())
            {
                case "Ingoing":
                    filter = filterBuilder.Gte("DateArrived", startDate) & filterBuilder.Lte("DateArrived", endDate);
                    break;
                case "Outgoing":
                    filter = filterBuilder.Gte("DateDeparted", startDate) & filterBuilder.Lte("DateDeparted", endDate);
                    break;
                case "Deceased":
                    filter = filterBuilder.Gte("DateDeceased", startDate) & filterBuilder.Lte("DateDeceased", endDate);
                    break;
                default:
                    return null;
            }

            try
            {
                // Search wild birds 
                var reportResults = collection.Find(filter).ToList();
                // Deserialize bson documents 
                List<CaptiveBird> results = new List<CaptiveBird>();
                foreach (var result in reportResults)
                {
                    results.Add(BsonSerializer.Deserialize<CaptiveBird>(result));
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

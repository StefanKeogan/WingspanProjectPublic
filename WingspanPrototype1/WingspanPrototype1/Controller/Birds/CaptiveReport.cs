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

            if (database != null)
            {
                // Get collection
                var collection = database.GetCollection<BsonDocument>("CaptiveBirds");

                // Used to build reports 
                var filterBuilder = Builders<BsonDocument>.Filter;

                FilterDefinition<BsonDocument> filter = null;

                // What search condition are we using?
                switch (condition.ToString())
                {
                    case "Ingoing":
                        filter = filterBuilder.Gte("DateArrived", startDate) & filterBuilder.Lte("DateArrived", endDate.ToLocalTime());
                        break;
                    case "Outgoing":
                        filter = filterBuilder.Gte("DateDeparted", startDate) & filterBuilder.Lte("DateDeparted", endDate.ToLocalTime());
                        break;
                    case "Deceased":
                        filter = filterBuilder.Gte("DateDeceased", startDate) & filterBuilder.Lte("DateDeceased", endDate.ToLocalTime());
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
            else
            {
                return null;
            }           

        }

        public static List<CaptiveBird> SponsoredReport(DateTime startDate, DateTime endDate)
        {
            // Get database
            var database = DatabaseConnection.GetDatabase();

            if (database != null)
            {
                // Get collection
                var collection = database.GetCollection<BsonDocument>("Sponsorships");

                // Used to build reports 
                var filterBuilder = Builders<BsonDocument>.Filter;

                // Build master filter
                var filter = filterBuilder.Gte("StartDate", startDate.ToLocalTime()) & filterBuilder.Lte("StartDate", endDate.ToLocalTime());

                try
                {
                    // Search wild birds 
                    var reportResults = collection.Find(filter).ToList();
                    // Deserialize bson documents 
                    List<CaptiveBird> captiveResults = new List<CaptiveBird>();
                    var wildCollection = database.GetCollection<BsonDocument>("CaptiveBirds");

                    foreach (var result in reportResults)
                    {
                        // Get sponsorship
                        Sponsorship sponsorship = BsonSerializer.Deserialize<Sponsorship>(result);

                        // Does the sponsorship match a saved wild bird?
                        var bird = wildCollection.Find(Builders<BsonDocument>.Filter.Eq("WingspanId", sponsorship.WingspanId)).FirstOrDefault();

                        if (bird != null)
                        {
                            CaptiveBird captiveBird = BsonSerializer.Deserialize<CaptiveBird>(bird);
                            captiveResults.Add(captiveBird);
                        }             

                    }

                    return captiveResults;
                }
                catch (Exception)
                {
                    throw;
                    // return null;
                }
            }
            else
            {
                return null;
            }

        }

    }
   
}

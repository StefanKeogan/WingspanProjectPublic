using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using WingspanPrototype1.Functions;
using WingspanPrototype1.Model;

namespace WingspanPrototype1.Controller.Birds
{
    class WildReport
    {       
        public static List<WildBird> BandedReport(DateTime startDate, DateTime endDate)
        {
            // Get database
            var database = DatabaseConnection.GetDatabase();

            if (database != null)
            {
                // Get collection
                var collection = database.GetCollection<BsonDocument>("WildBirds");

                // Used to build reports 
                var filterBuilder = Builders<BsonDocument>.Filter;

                // Build master filter
                var filter = filterBuilder.Gte("DateBanded", startDate.ToLocalTime()) & filterBuilder.Lte("DateBanded", endDate.ToLocalTime());

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
            else
            {
                return null;
            }                           
            
        }

        public static List<WildBird> SponsoredReport(DateTime startDate, DateTime endDate)
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
                    List<BsonDocument> reportResults = collection.Find(filter).ToList();
                    // Deserialize bson documents 
                    List<WildBird> wildResults = new List<WildBird>();
                    var wildCollection = database.GetCollection<BsonDocument>("WildBirds");

                    if (reportResults != null)
                    {
                        if (reportResults.Count > 0)
                        {
                            foreach (var result in reportResults)
                            {
                                // Get sponsorship
                                Sponsorship sponsorship = BsonSerializer.Deserialize<Sponsorship>(result);

                                // Does the sponsorship match a saved wild bird?
                                var bird = wildCollection.Find(Builders<BsonDocument>.Filter.Eq("WingspanId", sponsorship.WingspanId)).FirstOrDefault();
                                if (bird != null)
                                {
                                    WildBird wildBird = BsonSerializer.Deserialize<WildBird>(bird);
                                    wildResults.Add(wildBird);
                                }
                                else
                                {
                                    continue;
                                }

                            }

                            return wildResults;
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

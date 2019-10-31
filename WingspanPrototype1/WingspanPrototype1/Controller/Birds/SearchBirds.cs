using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using WingspanPrototype1.Functions;
using WingspanPrototype1.Model;

namespace WingspanPrototype1.Controller.Birds
{
     class SearchBirds
     {

         public static ArrayList Search(string wingspanId, string birdName, string bandNumber)
         {
            // Store both wild and captive results 
            ArrayList results = new ArrayList();

            // Get DB connection
            var database = DatabaseConnection.GetDatabase();

            if (database != null)
            {
                // Get captive bird collection 
                var captiveBirdsCollection = database.GetCollection<BsonDocument>("CaptiveBirds");

                // Get wild birds collection 
                var wildBirdsCollection = database.GetCollection<BsonDocument>("WildBirds");

                // Used to build filter with multiple conditions 
                var filterBuilder = Builders<BsonDocument>.Filter;

                // If both of these search feilds are not empty
                if (Validate.FeildPopulated(wingspanId) || Validate.FeildPopulated(birdName))
                {
                    if (captiveBirdsCollection != null)
                    {

                        List<FilterDefinition<BsonDocument>> filters = new List<FilterDefinition<BsonDocument>>();

                        if (Validate.FeildPopulated(wingspanId)) filters.Add(filterBuilder.Eq("WingspanId", wingspanId.Replace(" ", string.Empty)));
                        if (Validate.FeildPopulated(birdName)) filters.Add(filterBuilder.Eq("Name", birdName.Replace(" ", string.Empty).ToLower()));

                        // Captive bird filters with default values of null
                        FilterDefinition<BsonDocument> searchFilter = filters[0];

                        if (filters.Count > 0)
                        {
                            for (int i = 1; i < filters.Count; i++)
                            {
                                searchFilter |= filters[i];
                            }
                        }

                        try
                        {
                            // Search captive collection
                            List<BsonDocument> captiveResults = captiveBirdsCollection.Find(searchFilter).ToList();
                            // Convert results to captive bird object 
                            foreach (var result in captiveResults)
                            {
                                results.Add(BsonSerializer.Deserialize<CaptiveBird>(result));
                            }

                        }
                        catch (Exception)
                        {
                            return null;
                        }

                    }
                }

                // If both these feilds are not empty 
                if ((Validate.FeildPopulated(wingspanId)) || (Validate.FeildPopulated(bandNumber)))
                {
                    if (wildBirdsCollection != null)
                    {
                        List<FilterDefinition<BsonDocument>> filters = new List<FilterDefinition<BsonDocument>>();

                        // If a feild is populated add a condition to the filter
                        if (Validate.FeildPopulated(wingspanId)) filters.Add(filterBuilder.Eq("WingspanId", wingspanId.Replace(" ", string.Empty)));
                        if (Validate.FeildPopulated(bandNumber)) filters.Add(filterBuilder.Eq("MetalBand", bandNumber.Replace(" ", string.Empty)));

                        FilterDefinition<BsonDocument> searchFilter = filters[0];

                        if (filters.Count > 0)
                        {
                            for (int i = 1; i < filters.Count; i++)
                            {
                                searchFilter |= filters[i];
                            }
                        }

                        try
                        {
                            List<BsonDocument> wildresults = wildBirdsCollection.Find(searchFilter).ToList();
                            // Convert results to captive bird object 
                            foreach (var result in wildresults)
                            {
                                results.Add(BsonSerializer.Deserialize<WildBird>(result));
                            }

                        }
                        catch (Exception)
                        {

                            throw;
                        }

                    }

                }

                if (results.Count > 0) // Search successful return results
                {
                    return results;
                }
                else // Search failed return null
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

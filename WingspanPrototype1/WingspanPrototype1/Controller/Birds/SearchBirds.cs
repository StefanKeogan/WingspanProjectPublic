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
                    // Captive bird filters with default values of null
                    FilterDefinition<BsonDocument> captiveIdFilter = null;
                    FilterDefinition<BsonDocument> captiveNameFilter = null;

                    FilterDefinition<BsonDocument> captiveSearchFilter = null;

                    // If a feild is populated create filters
                    if (Validate.FeildPopulated(wingspanId)) captiveIdFilter = filterBuilder.Eq("WingspanId", wingspanId);
                    if (Validate.FeildPopulated(birdName)) captiveNameFilter = filterBuilder.Eq("Name", birdName);

                    // Build final filter
                    // Are we searching for both conditions?
                    if (captiveIdFilter != null && captiveNameFilter != null)                    
                    captiveSearchFilter = captiveIdFilter | captiveNameFilter;
                    else if (captiveIdFilter != null)
                    captiveSearchFilter = captiveIdFilter;                    
                    else if (captiveNameFilter != null)
                    captiveSearchFilter = captiveNameFilter;
                    
                    try
                    {
                        // Search captive collection
                        List<BsonDocument> captiveResults = captiveBirdsCollection.Find(captiveSearchFilter).ToList();
                        // Convert results to captive bird object 
                        foreach (var result in captiveResults)
                        {
                            results.Add(BsonSerializer.Deserialize<CaptiveBird>(result));
                        }

                    }
                    catch (Exception)
                    {
                        throw;
                    }

                }
            }

            // If both these feilds are not empty 
            if ((Validate.FeildPopulated(wingspanId)) || (Validate.FeildPopulated(bandNumber)))
            {
                if (wildBirdsCollection != null)
                {
                    // Wild bird filter
                    FilterDefinition<BsonDocument> wildIdFilter = null;
                    FilterDefinition<BsonDocument> wildNumberFilter = null;

                    FilterDefinition<BsonDocument> wildSearchFilter = null;

                    // If a feild is populated add a condition to the filter
                    if (Validate.FeildPopulated(wingspanId)) wildIdFilter = filterBuilder.Eq("WingspanId", wingspanId);
                    if (Validate.FeildPopulated(bandNumber)) wildNumberFilter = filterBuilder.Eq("BandNo", bandNumber);

                    // Are we searching for both conditions
                    if (wildIdFilter != null && wildNumberFilter != null) wildSearchFilter = wildIdFilter | wildNumberFilter;
                    else if (wildIdFilter != null) wildSearchFilter = wildIdFilter;
                    else if (wildNumberFilter != null) wildSearchFilter = wildNumberFilter;

                    try
                    {
                        List<BsonDocument> wildresults = wildBirdsCollection.Find(wildSearchFilter).ToList();
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
         
     }
    
}

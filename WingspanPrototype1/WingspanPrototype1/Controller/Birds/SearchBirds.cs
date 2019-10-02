using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using WingspanPrototype1.Functions;
using WingspanPrototype1.Model;

namespace WingspanPrototype1.Controller.Birds
{
    class SearchBirds
    {
        SearchBirds(string wingspanIdValue, string birdNameValue, string bandNumberValue)
        {   
            // Get DB connection
            var database = DatabaseConnection.GetDatabase();

            // Get each bird collection 
            var wildBirdsCollection = database.GetCollection<BsonDocument>("WildBirds");
            var captiveBirdsCollection = database.GetCollection<BsonDocument>("CaptiveBirds");

            var filterBuilder = Builders<BsonDocument>.Filter;

            // Wild bird filter
            var wildFilter = filterBuilder.Eq("WingspanId", wingspanIdValue) | filterBuilder.Eq("WildBand", bandNumberValue);

            // Captive bird filter
            var captiveFilter = filterBuilder.Eq("WingspanId", wingspanIdValue) | filterBuilder.Eq("CaptiveBandNo", bandNumberValue);

            // Search wild birds collection 
            List<BsonDocument> wildResults = wildBirdsCollection.Find(wildFilter).ToList();

            // Search captive birds collection 
            List<BsonDocument> captive = captiveBirdsCollection.Find(captiveFilter).ToList();

            if (wildResults != null)
            {
                foreach (var bird in wildResults)
                {
                    // TODO: Convert bson to bird 
                }
            }





        }

        private void ReturnSearchResults()
        {

        }
    }

    
}

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
     class SearchBirds
     {
         public string WingspanIdValue { get; set; }
         public string BirdNameValue { get; set; }
         public string BandNumberValue { get; set; }

         public SearchBirds(string wingspanId, string birdName, string bandNumber)
         {
             WingspanIdValue = wingspanId;
             BirdNameValue = birdName;
             BandNumberValue = bandNumber;

         }

         public List<CaptiveBird> SearchCaptiveBirds()
         {
             // Get DB connection
             var database = DatabaseConnection.GetDatabase();

             // Get each bird collection 
             var captiveBirdsCollection = database.GetCollection<BsonDocument>("CaptiveBirds");

             var filterBuilder = Builders<BsonDocument>.Filter;

             // Captive bird filter
             var captiveFilter = filterBuilder.Eq("WingspanId", WingspanIdValue) | filterBuilder.Eq("CaptiveBandNo", BandNumberValue);

             // Search captive birds collection 
             List<BsonDocument> captiveResults = captiveBirdsCollection.Find(captiveFilter).ToList();

             // Stores deserialised results
             List<CaptiveBird> captiveBirds = new List<CaptiveBird>();
             
             // Deserialise captive bird results
             if (captiveResults != null)
             {
                
                 foreach (var bird in captiveResults)
                 {
                     captiveBirds.Add(BsonSerializer.Deserialize<CaptiveBird>(bird));
                 }
             }
             else
             {
                 return null;
             }

             return captiveBirds;

         }

         public List<WildBird> SearchWildBirds()
         {
             // Get DB connection
             var database = DatabaseConnection.GetDatabase();

             // Get each bird collection 
             var wildBirdsCollection = database.GetCollection<BsonDocument>("WildBirds");

             var filterBuilder = Builders<BsonDocument>.Filter;

             // Wild bird filter
             var wildFilter = filterBuilder.Eq("WingspanId", WingspanIdValue) | filterBuilder.Eq("WildBand", BandNumberValue);

             // Search wild birds collection 
             List<BsonDocument> wildResults = wildBirdsCollection.Find(wildFilter).ToList();

             // Stores deserialised results
             List<WildBird> wildBirds = new List<WildBird>();

             // Deserialise wild bird results
             if (wildResults != null)
             {
                 foreach (var bird in wildResults)
                 {
                     wildBirds.Add(BsonSerializer.Deserialize<WildBird>(bird));
                 }
             }
             else
             {
                 return null;
             }

             return wildBirds;


         }
     }

    
}

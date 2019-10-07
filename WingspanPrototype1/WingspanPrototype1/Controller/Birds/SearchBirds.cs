﻿using MongoDB.Bson;
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

            // Used to build filter with multiple conditions 
             var filterBuilder = Builders<BsonDocument>.Filter;

             // Captive bird filter
             var captiveFilter = filterBuilder.Eq("WingspanId", WingspanIdValue) | filterBuilder.Eq("BandNo", BandNumberValue);

             // Search captive birds collection using filter
             List<BsonDocument> captiveResults = captiveBirdsCollection.Find(captiveFilter).ToList();          
             
             // Deserialise and return captive bird results
             if (captiveResults != null)
             {
                // Stores deserialised results
                List<CaptiveBird> captiveBirds = new List<CaptiveBird>();
                foreach (var bird in captiveResults)
                {
                     captiveBirds.Add(BsonSerializer.Deserialize<CaptiveBird>(bird));
                }
                return captiveBirds;
             }
             else
             {
                 return null;
             }

         }

        public List<WildBird> SearchWildBirds()
        {
            // Get DB connection
            var database = DatabaseConnection.GetDatabase();

            // Get each bird collection 
            var wildBirdsCollection = database.GetCollection<WildBird>("WildBirds");

            // Used to build filter with multiple conditions 
            var filterBuilder = Builders<WildBird>.Filter;

            FilterDefinition<WildBird> filter = null;

            if (Validate.FeildPopulated(WingspanIdValue)) filter |= filterBuilder.Eq(bird => bird.WingspanId, WingspanIdValue);

            // Deserialise wild bird results
            //if (wildResults != null)
            //{
            //    foreach (var bird in wildResults)
            //    {
            //        wildBirds.Add(BsonSerializer.Deserialize<WildBird>(bird));
            //    }
            //}
            //else
            //{
            //    return null;
            //}

            var wildBirds = wildBirdsCollection.Find(filter);

            return null;


         }
     }

    
}

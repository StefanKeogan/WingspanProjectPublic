﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using WingspanPrototype1.Model;

namespace WingspanPrototype1.Functions
{
    class GenerateWildWingspanId
    {
        public static string NewId()
        {
            // Get database
            DatabaseConnection databaseConnection = new DatabaseConnection();
            var database = databaseConnection.GetDatabase();

            if (database != null)
            {
                // Get collection
                var collection = database.GetCollection<BsonDocument>("WildIdValue");

                try
                {
                    // Get value
                    var value = collection.Find(new BsonDocument { }).First();

                    // Get Id Value 
                    var idValue = BsonSerializer.Deserialize<IdValue>(value);

                    // Generate Wingspan ID
                    int number = idValue.Value;
                    int year = DateTime.Today.Year % 100;
                    string wingspanId = "w" + year.ToString() + "/" + number.ToString();

                    int increment = number + 1;

                    // Increment Id Value 
                    collection.UpdateOne(Builders<BsonDocument>.Filter.Eq("_id", idValue.Value_id), Builders<BsonDocument>.Update.Set("Value", increment));

                    return wingspanId;
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

using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;
using WingspanPrototype1.Model;

namespace WingspanPrototype1.Controller.Birds
{
    class AddBirdLocation
    {
        public static bool InsertLocationDocument(Location location)
        {
            // Get database
            DatabaseConnection databaseConnection = new DatabaseConnection();
            var database = databaseConnection.GetDatabase();

            if (database != null)
            {
                // Get Note history collection
                var collection = database.GetCollection<BsonDocument>("LocationHistory");

                // Create document
                var document = new BsonDocument
                {
                    { "Date", location.Date },
                    { "Category", location.Category },
                    { "BirdLocation", location.BirdLocation },
                    { "WingspanId", location.WingspanId}
                };

                // Insert docunment
                try
                {
                    collection.InsertOne(document);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

            


        }
    }
}

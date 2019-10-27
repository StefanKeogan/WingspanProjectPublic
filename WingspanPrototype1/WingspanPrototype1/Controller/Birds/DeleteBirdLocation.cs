using System;
using System.Collections.Generic;
using System.Text;
using WingspanPrototype1.Functions;
using MongoDB.Bson;
using MongoDB.Driver;

namespace WingspanPrototype1.Controller.Birds
{
    class DeleteBirdLocation
    {
        public static bool DropDocument(ObjectId id)
        {
            // Get database 
            var database = DatabaseConnection.GetDatabase();

            if (database != null)
            {
                // Get location collection
                var collection = database.GetCollection<BsonDocument>("LocationHistory");

                // Build filter
                var filter = Builders<BsonDocument>.Filter.Eq("_id", id);

                // Delete document
                try
                {
                    collection.DeleteOne(filter);
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

using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace WingspanPrototype1.Controller.Birds
{
    class DeleteCaptiveBird
    {
        public static bool DropDocument(ObjectId id)
        {
            // Get database
            var database = DatabaseConnection.GetDatabase();

            if (database != null)
            {
                // Get Captive birds collection
                var collection = database.GetCollection<BsonDocument>("CaptiveBirds");

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

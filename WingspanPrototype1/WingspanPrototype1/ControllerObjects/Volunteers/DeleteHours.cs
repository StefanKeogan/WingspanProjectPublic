using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace WingspanPrototype1.Controller.Volunteers
{
    class DeleteHours
    {
        public static bool DropDocument(ObjectId id)
        {
            // Get database
            DatabaseConnection databaseConnection = new DatabaseConnection();
            var database = databaseConnection.GetDatabase();

            if (database != null)
            {
                // Get hours collection
                var collection = database.GetCollection<BsonDocument>("VolunteerHours");

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

using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;

namespace WingspanPrototype1.Controller.Sponsorships
{
    class DeleteSponsorship
    {
        public static bool DropDocument(ObjectId id)
        {
            // Get database
            DatabaseConnection databaseConnection = new DatabaseConnection();
            var database = databaseConnection.GetDatabase();

            // Get collection
            var collection = database.GetCollection<BsonDocument>("Sponsorships");

            // Create filter
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
    }
}

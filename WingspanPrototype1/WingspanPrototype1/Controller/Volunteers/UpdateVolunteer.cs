using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using WingspanPrototype1.Functions;
using Xamarin.Forms;

namespace WingspanPrototype1.Controller.Volunteers
{
    class UpdateVolunteer
    {
        public static bool UpdateDocument(ObjectId id, List<Entry> entries)
        {
            // Get database 
            var database = DatabaseConnection.GetDatabase();

            // Get collection 
            var collection = database.GetCollection<BsonDocument>("Volunteers");

            // Create update builder 
            var updateBuilder = Builders<BsonDocument>.Update;

            // Is our collection null?
            if (collection != null)
            {
                foreach (var entry in entries)
                {
                    // Update document id entries are populated 
                    if (Validate.FeildPopulated(entry.Text))
                    {
                        if (entry.StyleId == "emailEntry") collection.UpdateOne(Builders<BsonDocument>.Filter.Eq("_id", id), updateBuilder.Set("Email", entry.Text));
                        if (entry.StyleId == "nameEntry") collection.UpdateOne(Builders<BsonDocument>.Filter.Eq("_id", id), updateBuilder.Set("Name", entry.Text));
                        if (entry.StyleId == "mobileEntry") collection.UpdateOne(Builders<BsonDocument>.Filter.Eq("_id", id), updateBuilder.Set("Mobile", entry.Text));
                    }
                }

                return true;
            }

            return false;
        }
    }
}

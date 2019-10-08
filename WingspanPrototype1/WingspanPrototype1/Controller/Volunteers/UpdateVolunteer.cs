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
            var database = DatabaseConnection.GetDatabase();

            var collection = database.GetCollection<BsonDocument>("Volunteers");

            var updateBuilder = Builders<BsonDocument>.Update;

            if (collection != null)
            {
                foreach (var entry in entries)
                {
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

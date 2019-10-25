using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using WingspanPrototype1.Functions;
using WingspanPrototype1.Model;
using Xamarin.Forms;

namespace WingspanPrototype1.Controller.Volunteers
{
    class UpdateVolunteer
    {
        public static Volunteer UpdateDocument(ObjectId id, List<Entry> entries)
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
                        try
                        {
                            if (entry.StyleId == "volunteerEmailEntry") collection.UpdateOne(Builders<BsonDocument>.Filter.Eq("_id", id), updateBuilder.Set("Email", entry.Text.Replace(" ", string.Empty)));
                            if (entry.StyleId == "volunteerFirstNameEntry") collection.UpdateOne(Builders<BsonDocument>.Filter.Eq("_id", id), updateBuilder.Set("FirstName", entry.Text.Replace(" ", string.Empty).ToLower()));
                            if (entry.StyleId == "volunteerLastNameEntry") collection.UpdateOne(Builders<BsonDocument>.Filter.Eq("_id", id), updateBuilder.Set("LastName", entry.Text.Replace(" ", string.Empty).ToLower()));
                            if (entry.StyleId == "volunteerMobileEntry") collection.UpdateOne(Builders<BsonDocument>.Filter.Eq("_id", id), updateBuilder.Set("Mobile", entry.Text));
                        }
                        catch (Exception)
                        {

                            return null;
                        }
                        
                    }
                }

                return BsonSerializer.Deserialize<Volunteer>(collection.Find(Builders<BsonDocument>.Filter.Eq("_id", id)).First()); ;
            }
            else
            {
                return null;
            }

            
        }
    }
}

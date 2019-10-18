using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using WingspanPrototype1.Functions;
using Xamarin.Forms;

namespace WingspanPrototype1.Controller.Birds
{
    class UpdateWildBird
    {
        public static WildBird UpdateDocument(ObjectId id, List<Entry> entries, List<Picker> pickers) // TODO: Add date time update functionality 
        {
            // Get database
            var database = DatabaseConnection.GetDatabase();

            // Get member collection
            var collection = database.GetCollection<BsonDocument>("WildBirds");

            // Use to build updates
            var updateBuilder = Builders<BsonDocument>.Update;

            // If we are able to get collection
            if (collection != null)
            {
                // Are any entries being updated?
                if (entries.Count > 0)
                {
                    foreach (var entry in entries)
                    {
                        // Is the entry poulated
                        if (Validate.FeildPopulated(entry.Text))
                        {
                            try
                            {
                                // Which feild are we updateing?
                                if (entry.StyleId == "wildWingspanIdEntry") collection.UpdateOne(Builders<BsonDocument>.Filter.Eq("_id", id), updateBuilder.Set("WingspanId", entry.Text));
                                if (entry.StyleId == "wildLocationEntry") collection.UpdateOne(Builders<BsonDocument>.Filter.Eq("_id", id), updateBuilder.Set("Location", entry.Text));
                                if (entry.StyleId == "wildGpsEntry") collection.UpdateOne(Builders<BsonDocument>.Filter.Eq("_id", id), updateBuilder.Set("Gps", entry.Text));
                                if (entry.StyleId == "wildMetalBandIdEntry") collection.UpdateOne(Builders<BsonDocument>.Filter.Eq("_id", id), updateBuilder.Set("MetalBand", entry.Text));
                                if (entry.StyleId == "bandInfoEntry") collection.UpdateOne(Builders<BsonDocument>.Filter.Eq("_id", id), updateBuilder.Set("BandInfo", entry.Text));
                                if (entry.StyleId == "wildBanderNameEntry") collection.UpdateOne(Builders<BsonDocument>.Filter.Eq("_id", id), updateBuilder.Set("BanderName", entry.Text));
                            }
                            catch (Exception)
                            {
                                return null;
                            }                            

                        }
                    }

                }

                // Are there any pickers being updated
                if (pickers.Count > 0)
                {
                    foreach (var picker in pickers)
                    {
                        try
                        {
                            if (picker.StyleId == "wildSpeciesPicker") collection.UpdateOne(Builders<BsonDocument>.Filter.Eq("_id", id), updateBuilder.Set("WingspanId", picker.SelectedItem.ToString()));
                            if (picker.StyleId == "wildSexPicker") collection.UpdateOne(Builders<BsonDocument>.Filter.Eq("_id", id), updateBuilder.Set("Sex", picker.SelectedItem.ToString()));
                            if (picker.StyleId == "wildAgePicker") collection.UpdateOne(Builders<BsonDocument>.Filter.Eq("_id", id), updateBuilder.Set("Age", picker.SelectedItem.ToString()));
                        }
                        catch (Exception)
                        {
                            return null;
                        }
                        
                    }
                }

                return BsonSerializer.Deserialize<WildBird>(collection.Find(Builders<BsonDocument>.Filter.Eq("_id", id)).First());
                
            }
            else
            {
                return null;
            }

        }

    }
}

using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using WingspanPrototype1.Functions;
using Xamarin.Forms;


namespace WingspanPrototype1.Controller.Birds
{
    class UpdateCaptiveBird
    {
        public static bool UpdateDocument(ObjectId id, List<Entry> entries, List<Picker> pickers) // TODO: Add date time update functionality 
        {
            // Get database
            var database = DatabaseConnection.GetDatabase();

            // Get member collection
            var collection = database.GetCollection<BsonDocument>("CaptiveBirds");

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
                            // Which feild are we updateing?
                            if (entry.StyleId == "captiveWingspanIdEntry") collection.UpdateOne(Builders<BsonDocument>.Filter.Eq("_id", id), updateBuilder.Set("WingspanId", entry.Text));
                            if (entry.StyleId == "captiveNameEntry") collection.UpdateOne(Builders<BsonDocument>.Filter.Eq("_id", id), updateBuilder.Set("Name", entry.Text));
                            if (entry.StyleId == "captiveBandNumberEntry") collection.UpdateOne(Builders<BsonDocument>.Filter.Eq("_id", id), updateBuilder.Set("BandNumber", entry.Text));
                            if (entry.StyleId == "captiveLocationEntry") collection.UpdateOne(Builders<BsonDocument>.Filter.Eq("_id", id), updateBuilder.Set("Location", entry.Text));
                            if (entry.StyleId == "captiveResultEntry") collection.UpdateOne(Builders<BsonDocument>.Filter.Eq("_id", id), updateBuilder.Set("Result", entry.Text));

                        }
                    }

                    return true;
                }

                // Are there any pickers being updated
                if (pickers.Count > 0)
                {
                    foreach (var picker in pickers)
                    {
                        if (picker.StyleId == "captiveBandColourPicker") collection.UpdateOne(Builders<BsonDocument>.Filter.Eq("_id", id), updateBuilder.Set("BandColour", picker.SelectedItem.ToString()));
                        if (picker.StyleId == "captiveSpeciesPicker") collection.UpdateOne(Builders<BsonDocument>.Filter.Eq("_id", id), updateBuilder.Set("Species", picker.SelectedItem.ToString()));
                        if (picker.StyleId == "captiveSexPicker") collection.UpdateOne(Builders<BsonDocument>.Filter.Eq("_id", id), updateBuilder.Set("Sex", picker.SelectedItem.ToString()));
                        if (picker.StyleId == "captiveAgePicker") collection.UpdateOne(Builders<BsonDocument>.Filter.Eq("_id", id), updateBuilder.Set("Age", picker.SelectedItem.ToString()));
                    }
                }

                return true;

            }
            else
            {
                return false;
            }

        }
    }
}

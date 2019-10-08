using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using WingspanPrototype1.Functions;
using Xamarin.Forms;

namespace WingspanPrototype1.Controller.Members
{
    class UpdateMember
    {
        public static bool UpdateDocument(ObjectId id, List<Entry> entries, Editor editor) // TODO: Add date time update functionality 
        {
            // Get database
            var database = DatabaseConnection.GetDatabase();

            // Get member collection
            var collection = database.GetCollection<BsonDocument>("Members");

            // Use to build updates
            var updateBuilder = Builders<BsonDocument>.Update;

            // If we are able to get collection
            if (collection != null)
            {
                // Are any entried being updated?
                if (entries.Count > 0)
                {
                    foreach (var entry in entries)
                    {
                        // Is the entry poulated
                        if (Validate.FeildPopulated(entry.Text))
                        {
                            // Which feild are we updateing?
                            if (entry.StyleId == "memberFirstNameEntry") collection.UpdateOne(Builders<BsonDocument>.Filter.Eq("_id", id), updateBuilder.Set("FirstName", entry.Text));
                            if (entry.StyleId == "memberLastNameEntry") collection.UpdateOne(Builders<BsonDocument>.Filter.Eq("_id", id), updateBuilder.Set("LasName", entry.Text));
                            if (entry.StyleId == "memberSalutationEntry") collection.UpdateOne(Builders<BsonDocument>.Filter.Eq("_id", id), updateBuilder.Set("SalutationName", entry.Text));
                            if (entry.StyleId == "memberEmailEntry") collection.UpdateOne(Builders<BsonDocument>.Filter.Eq("_id", id), updateBuilder.Set("Email", entry.Text));
                            if (entry.StyleId == "memberCompanyEntry") collection.UpdateOne(Builders<BsonDocument>.Filter.Eq("_id", id), updateBuilder.Set("Company", entry.Text));
                            if (entry.StyleId == "memberAddress1Entry") collection.UpdateOne(Builders<BsonDocument>.Filter.Eq("_id", id), updateBuilder.Set("Address1", entry.Text));
                            if (entry.StyleId == "memberAddress2Entry") collection.UpdateOne(Builders<BsonDocument>.Filter.Eq("_id", id), updateBuilder.Set("Address2", entry.Text));
                            if (entry.StyleId == "memberAddress3Entry") collection.UpdateOne(Builders<BsonDocument>.Filter.Eq("_id", id), updateBuilder.Set("Address3", entry.Text));
                            if (entry.StyleId == "memberCityEntry") collection.UpdateOne(Builders<BsonDocument>.Filter.Eq("_id", id), updateBuilder.Set("City", entry.Text));
                            if (entry.StyleId == "memberPostCodeEntry") collection.UpdateOne(Builders<BsonDocument>.Filter.Eq("_id", id), updateBuilder.Set("PostCode", entry.Text));
                        }
                    }

                    // Are we updating the comment feild?
                    if (Validate.FeildPopulated(editor.Text))
                    {
                        collection.UpdateOne(Builders<BsonDocument>.Filter.Eq("_id", id), updateBuilder.Set("Comment", editor.Text));
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

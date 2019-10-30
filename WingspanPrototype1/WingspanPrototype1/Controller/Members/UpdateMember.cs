using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using WingspanPrototype1.Functions;
using WingspanPrototype1.Model;
using Xamarin.Forms;

namespace WingspanPrototype1.Controller.Members
{
    class UpdateMember
    {
        public static Member UpdateDocument(ObjectId id, List<Entry> entries, Editor editor, DatePicker date)
        {
            // Get database
            var database = DatabaseConnection.GetDatabase();

            if (database != null)
            {
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
                                try
                                {
                                    // Which feild are we updating?
                                    if (entry.StyleId == "memberFirstNameEntry") collection.UpdateOne(Builders<BsonDocument>.Filter.Eq("_id", id), updateBuilder.Set("FirstName", entry.Text));
                                    if (entry.StyleId == "memberLastNameEntry") collection.UpdateOne(Builders<BsonDocument>.Filter.Eq("_id", id), updateBuilder.Set("LastName", entry.Text));
                                    if (entry.StyleId == "memberSalutationEntry") collection.UpdateOne(Builders<BsonDocument>.Filter.Eq("_id", id), updateBuilder.Set("SalutationName", entry.Text));
                                    if (entry.StyleId == "memberEmailEntry") collection.UpdateOne(Builders<BsonDocument>.Filter.Eq("_id", id), updateBuilder.Set("Email", entry.Text));
                                    if (entry.StyleId == "memberCompanyEntry") collection.UpdateOne(Builders<BsonDocument>.Filter.Eq("_id", id), updateBuilder.Set("Company", entry.Text));
                                    if (entry.StyleId == "memberAddress1Entry") collection.UpdateOne(Builders<BsonDocument>.Filter.Eq("_id", id), updateBuilder.Set("Address1", entry.Text));
                                    if (entry.StyleId == "memberAddress2Entry") collection.UpdateOne(Builders<BsonDocument>.Filter.Eq("_id", id), updateBuilder.Set("Address2", entry.Text));
                                    if (entry.StyleId == "memberAddress3Entry") collection.UpdateOne(Builders<BsonDocument>.Filter.Eq("_id", id), updateBuilder.Set("Address3", entry.Text));
                                    if (entry.StyleId == "memberCityEntry") collection.UpdateOne(Builders<BsonDocument>.Filter.Eq("_id", id), updateBuilder.Set("City", entry.Text));
                                    if (entry.StyleId == "memberPostCodeEntry") collection.UpdateOne(Builders<BsonDocument>.Filter.Eq("_id", id), updateBuilder.Set("Postcode", entry.Text));
                                    if (entry.StyleId == "memberCountryEntry") collection.UpdateOne(Builders<BsonDocument>.Filter.Eq("_id", id), updateBuilder.Set("Country", entry.Text));

                                }
                                catch (Exception)
                                {
                                    return null;
                                }
                            }

                        }

                    }

                    // Are we updating the comment feild?
                    if (editor != null)
                    {
                        try
                        {
                            collection.UpdateOne(Builders<BsonDocument>.Filter.Eq("_id", id), updateBuilder.Set("Comment", editor.Text));
                        }
                        catch (Exception)
                        {
                            return null;
                        }

                    }

                    if (date != null)
                    {
                        try
                        {
                            collection.UpdateOne(Builders<BsonDocument>.Filter.Eq("_id", id), updateBuilder.Set("JoinDate", date.Date));
                        }
                        catch (Exception)
                        {
                            return null;
                        }
                    }

                    // Return updated member
                    return BsonSerializer.Deserialize<Member>(collection.Find(Builders<BsonDocument>.Filter.Eq("_id", id)).First());

                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }         

        }
    }
}

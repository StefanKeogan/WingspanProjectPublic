using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using WingspanPrototype1.Controller.Birds;
using WingspanPrototype1.Functions;
using WingspanPrototype1.Model;
using Xamarin.Forms;

namespace WingspanPrototype1.Controller.Sponsorships
{
    class UpdateSponsorship
    {
        public static Sponsorship UpdateDocument(ObjectId sponsorshipId, ObjectId memberId, string bird, Picker category, Editor notes, DatePicker start, DatePicker end)
        {
            // Get database
            var database = DatabaseConnection.GetDatabase();

            if (database != null)
            {
                // Get sponsorship collection
                var collection = database.GetCollection<BsonDocument>("Sponsorships");

                // Use to build updates
                var updateBuilder = Builders<BsonDocument>.Update;

                try
                {
                    if (memberId != ObjectId.Empty) collection.UpdateOne(Builders<BsonDocument>.Filter.Eq("_id", sponsorshipId), updateBuilder.Set("Member_id", memberId));
                    if (bird != null) collection.UpdateOne(Builders<BsonDocument>.Filter.Eq("_id", sponsorshipId), updateBuilder.Set("WingspanId", bird));
                
                }
                catch (Exception)
                {

                    return null;
                }


                //are we updating the level of sponsorship?
                if (category.SelectedIndex != -1)
                {
                    try
                    {
                        collection.UpdateOne(Builders<BsonDocument>.Filter.Eq("_id", sponsorshipId), updateBuilder.Set("Category", category.SelectedItem.ToString()));
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }

                //are we updating the sponsorship notes?
                if (notes != null)
                {
                    try
                    {
                        collection.UpdateOne(Builders<BsonDocument>.Filter.Eq("_id", sponsorshipId), updateBuilder.Set("Notes", notes.Text));
                    }
                    catch (Exception)
                    {
                        return null;
                    }

                }

                //are we updating the start date?
                if (start != null)
                {
                    try
                    {
                        collection.UpdateOne(Builders<BsonDocument>.Filter.Eq("_id", sponsorshipId), updateBuilder.Set("StartDate", start.Date.ToLocalTime()));
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }

                //are we updating the end date?
                if (end != null)
                {
                    try
                    {
                        collection.UpdateOne(Builders<BsonDocument>.Filter.Eq("_id", sponsorshipId), updateBuilder.Set("EndDate", end.Date.ToLocalTime()));
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }

                // Return updated sponsorship
                return BsonSerializer.Deserialize<Sponsorship>(collection.Find(Builders<BsonDocument>.Filter.Eq("_id", sponsorshipId)).First());

            }
            else
            {
                return null;
            }
        }
    }
}

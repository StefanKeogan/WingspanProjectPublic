using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using WingspanPrototype1.Model;

namespace WingspanPrototype1.Controller.Volunteers
{
    class FindVolunteerWorkHours
    {
        public static List<VolunteerHours> GetHoursDocuments(ObjectId volunteerId)
        {
            // Get DB
            var database = DatabaseConnection.GetDatabase();

            if (database != null)
            {
                // Get payment collection
                var collection = database.GetCollection<BsonDocument>("VolunteerHours");

                try
                {
                    // Search payment collection 
                    List<BsonDocument> paymentResults = collection.Find(Builders<BsonDocument>.Filter.Eq("Volunteer_id", volunteerId)).ToList();

                    // Convert results to member results
                    List<VolunteerHours> hoursObjectResults = new List<VolunteerHours>();
                    foreach (var result in paymentResults)
                    {
                        hoursObjectResults.Add(BsonSerializer.Deserialize<VolunteerHours>(result));
                    }
                    return hoursObjectResults;
                }
                catch (Exception)
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

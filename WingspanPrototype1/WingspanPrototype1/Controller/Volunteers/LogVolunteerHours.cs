using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;
using WingspanPrototype1.Model;

namespace WingspanPrototype1.Controller.Volunteers
{
    class LogVolunteerHours
    {
        public static bool InsertVolunteerHoursDocument(VolunteerHours hours)
        {
            // Get databse
            var database = DatabaseConnection.GetDatabase();

            if (database != null)
            {
                // Get Note history collection
                var collection = database.GetCollection<BsonDocument>("VolunteerHours");

                // Create document
                var document = new BsonDocument
                {
                    { "Date", hours.Date },
                    { "Amount", hours.Amount },
                    { "Note", hours.Note },
                    { "Volunteer_id", hours.Volunteer_id}
                };

                // Insert docunment
                try
                {
                    collection.InsertOne(document);
                    return true;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
            {
                return false;
            }

        }
    }
}

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
            // Get database
            DatabaseConnection databaseConnection = new DatabaseConnection();
            var database = databaseConnection.GetDatabase();

            if (database != null)
            {
                // Get Note history collection
                var collection = database.GetCollection<BsonDocument>("VolunteerHours");

                // Create document
                var document = new BsonDocument
                {
                    { "Date", hours.Date.ToLocalTime() },
                    { "Amount", hours.Amount },
                    { "Volunteer_id", hours.Volunteer_id}
                };

                if (hours.Note != null) document.Add("Note", hours.Note); 

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

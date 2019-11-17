using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using WingspanPrototype1.Model;

namespace WingspanPrototype1.Controller.Volunteers
{
    class VolunteerReport
    {
        public static List<VolunteerHours> WorkedReport(DateTime startDate, DateTime endDate)
        {
            // Get database
            DatabaseConnection databaseConnection = new DatabaseConnection();
            var database = databaseConnection.GetDatabase();

            if (database != null)
            {
                var collection = database.GetCollection<BsonDocument>("VolunteerHours");

                var filterBuilder = Builders<BsonDocument>.Filter;

                var filter = filterBuilder.Gte("Date", startDate) & filterBuilder.Lte("Date", endDate);

                try
                {
                    List<BsonDocument> reportResults = collection.Find(filter).ToList();

                    List<VolunteerHours> resultObjects = new List<VolunteerHours>();
                    foreach (var result in reportResults)
                    {
                        resultObjects.Add(BsonSerializer.Deserialize<VolunteerHours>(result));
                    }
                    return resultObjects;

                }
                catch (Exception)
                {
                    throw;
                    //return null;
                }

            }
            else
            {
                return null;
            }

        }
    }
}

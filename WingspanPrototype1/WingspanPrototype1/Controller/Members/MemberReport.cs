using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using WingspanPrototype1.Model;

namespace WingspanPrototype1.Controller.Members
{
    class MemberReport
    {
        public static List<Member> JoinedReport(DateTime startDate, DateTime endDate)
        {
            // Get database
            var database = DatabaseConnection.GetDatabase();

            if (database != null)
            {
                // Get collection
                var collection = database.GetCollection<BsonDocument>("Members");

                // Used to build reports 
                var filterBuilder = Builders<BsonDocument>.Filter;

                // Build master filter
                var filter = filterBuilder.Gte("JoinDate", startDate) & filterBuilder.Lte("JoinDate", endDate);

                try
                {
                    // Search wild birds 
                    var reportResults = collection.Find(filter).ToList();
                    // Deserialize bson documents
                    List<Member> results = new List<Member>();
                    foreach (var result in reportResults)
                    {
                        results.Add(BsonSerializer.Deserialize<Member>(result));
                    }

                    return results;
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

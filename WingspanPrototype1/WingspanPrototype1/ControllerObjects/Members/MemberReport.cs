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
            DatabaseConnection databaseConnection = new DatabaseConnection();
            var database = databaseConnection.GetDatabase();

            if (database != null)
            {
                // Get collection
                var collection = database.GetCollection<BsonDocument>("Members");

                // Used to build reports 
                var filterBuilder = Builders<BsonDocument>.Filter;

                // Build master filter
                var filter = filterBuilder.Gte("JoinDate", startDate.ToLocalTime()) & filterBuilder.Lte("JoinDate", endDate.ToLocalTime());

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

        public static List<Payment> PaymentReport(DateTime startDate, DateTime endDate)
        {
            // Get database
            DatabaseConnection databaseConnection = new DatabaseConnection();
            var database = databaseConnection.GetDatabase();

            if (database != null)
            {
                var collection = database.GetCollection<BsonDocument>("Payments");

                var filterBuilder = Builders<BsonDocument>.Filter;

                // Build master filter
                var filter = filterBuilder.Gte("Date", startDate.ToLocalTime()) & filterBuilder.Lte("Date", endDate.ToLocalTime());

                try
                {
                    // Search wild birds 
                    var reportResults = collection.Find(filter).ToList();
                    // Deserialize bson documents
                    List<Payment> results = new List<Payment>();
                    foreach (var result in reportResults)
                    {
                        results.Add(BsonSerializer.Deserialize<Payment>(result));
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

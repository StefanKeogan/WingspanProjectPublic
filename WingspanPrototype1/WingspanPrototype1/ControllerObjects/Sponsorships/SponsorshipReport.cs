using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using WingspanPrototype1.Controller.Birds;
using WingspanPrototype1.Model;

namespace WingspanPrototype1.Controller.Sponsorships
{
    class SponsorshipReport
    {
        public static List<Sponsorship> StartedReport(DateTime startDate, DateTime endDate)
        {
            // Get database
            DatabaseConnection databaseConnection = new DatabaseConnection();
            var database = databaseConnection.GetDatabase();

            if (database != null)
            {
                var collection = database.GetCollection<BsonDocument>("Sponsorships");

                var filterBuilder = Builders<BsonDocument>.Filter;

                var filter = filterBuilder.Gte("StartDate", startDate) & filterBuilder.Lte("StartDate", endDate);

                try
                {
                    List<BsonDocument> results = collection.Find(filter).ToList();

                    List<Sponsorship> sponsorshipResults = new List<Sponsorship>();
                   
                    foreach (var result in results)
                    {
                        sponsorshipResults.Add(BsonSerializer.Deserialize<Sponsorship>(result));
                    }

                    return sponsorshipResults;

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

        public static List<Member> MembersSponsoring(DateTime startDate, DateTime endDate)
        {
            // Get database
            DatabaseConnection databaseConnection = new DatabaseConnection();
            var database = databaseConnection.GetDatabase();

            if (database != null)
            {
                var collection = database.GetCollection<BsonDocument>("Sponsorships");

                var filterBuilder = Builders<BsonDocument>.Filter;

                var filter = filterBuilder.Gte("StartDate", startDate) & filterBuilder.Lte("StartDate", endDate);

                try
                {
                    List<BsonDocument> results = collection.Find(filter).ToList();
                    List<Sponsorship> sponsorshipResults = new List<Sponsorship>();

                    foreach (var result in results)
                    {
                        sponsorshipResults.Add(BsonSerializer.Deserialize<Sponsorship>(result));
                    }

                    List<Member> memberResults = new List<Member>();

                    foreach (var sponsorship in sponsorshipResults)
                    {
                        memberResults.Add(SearchMembers.SearchById(sponsorship.Member_id));
                    }

                    return memberResults;

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

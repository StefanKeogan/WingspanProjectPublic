using System;
using System.Collections.Generic;
using System.Text;
using WingspanPrototype1.Model;
using WingspanPrototype1.Functions;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Bson.Serialization;

namespace WingspanPrototype1.Controller.Sponsorships
{
    class SearchSponsorships
    {
        public static List<Sponsorship> Search(string wingspanID, string firstName, string lastName, string companyName)
        {
            // Get DB
            var database = DatabaseConnection.GetDatabase();

            // Get sponsorship collection
            var collection = database.GetCollection<BsonDocument>("Sponsorships");

            // Used to build filter with multiple conditions
            var filterBuilder = Builders<BsonDocument>.Filter;

            List<FilterDefinition<BsonDocument>> filters = new List<FilterDefinition<BsonDocument>>();

            // If feilds are populated add conditions to the filter
            if (Validate.FeildPopulated(wingspanID)) filters.Add(filterBuilder.Eq("WingspanId", wingspanID));
            if (Validate.FeildPopulated(firstName)) filters.Add(filterBuilder.Eq("FirstName", firstName));
            if (Validate.FeildPopulated(lastName)) filters.Add(filterBuilder.Eq("LastName", lastName));
            if (Validate.FeildPopulated(companyName)) filters.Add(filterBuilder.Eq("Company", companyName));

            FilterDefinition<BsonDocument> searchFilter = filters[0];

            if (filters.Count > 1)
            {
                for (int i = 1; i < filters.Count; i++)
                {
                    searchFilter |= filters[i];
                }
            }

            try
            {
                // Search sponsorship collection 
                List<BsonDocument> sponsorshipResults = collection.Find(searchFilter).ToList();

                // Convert results to sponsorship results
                List<Sponsorship> sponsorshipObjectResults = new List<Sponsorship>();
                foreach (var result in sponsorshipResults)
                {
                    sponsorshipObjectResults.Add(BsonSerializer.Deserialize<Sponsorship>(result));
                }

                return sponsorshipObjectResults;
            }
            catch (Exception)
            {
                return null;
            }


        } 
    }
}

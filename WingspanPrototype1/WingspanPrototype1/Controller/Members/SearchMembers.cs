using System;
using System.Collections.Generic;
using System.Text;
using WingspanPrototype1.Model;
using WingspanPrototype1.Functions;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Bson.Serialization;

namespace WingspanPrototype1.Controller.Birds
{
    class SearchMembers
    {   
        public static List<Member> Search(string firstName, string lastName, string salutationName)
        {
            // Get DB
            var database = DatabaseConnection.GetDatabase();

            // Get member collection
            var collection = database.GetCollection<BsonDocument>("Members");

            // Used to build filter with multiple conditions
            var filterBuilder = Builders<BsonDocument>.Filter;

            List<FilterDefinition<BsonDocument>> filters = new List<FilterDefinition<BsonDocument>>();

            // If feilds are populated add conditions to the filter
            if (Validate.FeildPopulated(firstName)) filters.Add(filterBuilder.Eq("FirstName", firstName.ToLower()));
            if (Validate.FeildPopulated(lastName)) filters.Add(filterBuilder.Eq("LastName", lastName.ToLower()));
            if (Validate.FeildPopulated(salutationName)) filters.Add(filterBuilder.Eq("SalutionName", salutationName.ToLower()));

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
                // Search member collection 
                List<BsonDocument> memberResults = collection.Find(searchFilter).ToList();

                // Convert results to member results
                List<Member> memberObjectResults = new List<Member>();
                foreach (var result in memberResults)
                {
                    memberObjectResults.Add(BsonSerializer.Deserialize<Member>(result));
                }

                return memberObjectResults;
            }
            catch (Exception)
            {
                return null;
            }

        }

    }
}

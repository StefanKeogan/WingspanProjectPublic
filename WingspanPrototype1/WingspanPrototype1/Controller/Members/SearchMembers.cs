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
            var collection = database.GetCollection<Member>("Members");

            // Used to build filter with multiple conditions
            var filterBuilder = Builders<Member>.Filter;
        
            // Store as default for now
            FilterDefinition<Member> filter = null;

            // If feilds are populated add conditions to the filter
            if (Validate.FeildPopulated(firstName)) filter |= filterBuilder.Eq(member => member.FirstName, firstName);
            if (Validate.FeildPopulated(lastName)) filter |= filterBuilder.Eq(member => member.LastName, lastName);
            if (Validate.FeildPopulated(salutationName)) filter |= filterBuilder.Eq(member => member.SaluationName, salutationName);

            try
            {
                // Search member collection 
                var memberResults = collection.Find(filter).ToList();
                return memberResults;
            }
            catch (Exception)
            {
                return null;
            }

        }

    }
}

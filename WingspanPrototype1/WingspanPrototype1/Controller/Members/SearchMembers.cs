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

            // Build search filter
            // var filter = filterBuilder.Eq(member => member.FirstName, FirstName) | filterBuilder.Eq(member => member.LastName, LastName) | filterBuilder.Eq(member => member.SaluationName, SalutationName);

            // Store as default for now
            var filter = filterBuilder.Eq(member => member.FirstName, firstName);

            if (Validate.FeildPopulated(firstName))
            {
                filter = filter | filterBuilder.Eq(member => member.FirstName, firstName);
            }
            if (Validate.FeildPopulated(lastName))
            {
                filter = filter | filterBuilder.Eq(member => member.LastName, lastName);
            }
            if (Validate.FeildPopulated(salutationName))
            {
                filter = filter | filterBuilder.Eq(member => member.SaluationName, salutationName);
            }

            try
            {
                // Search member collection 
                List<Member> memberResults = collection.Find(filter).ToList();
                return memberResults;
            }
            catch (Exception)
            {
                return null;
            }

            


            

        }

    }
}

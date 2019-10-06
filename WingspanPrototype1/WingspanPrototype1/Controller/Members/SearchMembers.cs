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

            FilterDefinition<Member> filter = null;

            if (firstName != string.Empty)
            {
                filter = filter | filterBuilder.Eq(member => member.FirstName, firstName);
            }
            if (lastName != string.Empty)
            {
                filter = filter | filterBuilder.Eq(member => member.LastName, lastName);
            }
            if (salutationName != string.Empty)
            {
                filter = filter | filterBuilder.Eq(member => member.SaluationName, salutationName);
            }

            // Search member collection 
            List<Member> memberResults = collection.Find(filter).ToList();


            return memberResults;

        }

    }
}

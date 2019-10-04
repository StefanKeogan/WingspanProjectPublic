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
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SalutationName { get; set; }

        public SearchMembers(string firstName, string lastName, string salutationName)
        {
            FirstName = firstName;
            LastName = lastName;
            SalutationName = salutationName;
        }

        public List<Member> Search()
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

            if (FirstName != string.Empty)
            {
                filter = filter | filterBuilder.Eq(member => member.FirstName, FirstName);
            }
            if (LastName != string.Empty)
            {
                filter = filter | filterBuilder.Eq(member => member.LastName, LastName);
            }
            if (SalutationName != string.Empty)
            {
                filter = filter | filterBuilder.Eq(member => member.SaluationName, SalutationName);
            }

            // Search member collection 
            List<Member> memberResults = collection.Find<Member>(filter).ToList();

            //// Store member objects 
            //List<Member> members = new List<Member>();

            //// If members have been found, deserialise and store as member objects
            //if (memebrResults != null)
            //{
            //    foreach (var member in memebrResults)
            //    {
            //        members.Add(BsonSerializer.Deserialize<Member>(member));
            //    }
            //}
            //else
            //{
            //    return null;
            //}

            return memberResults;

        }

    }
}

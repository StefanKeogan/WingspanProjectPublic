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

            if (database != null)
            {
                // Get member collection
                var collection = database.GetCollection<BsonDocument>("Members");

                // Used to build filter with multiple conditions
                var filterBuilder = Builders<BsonDocument>.Filter;

                List<FilterDefinition<BsonDocument>> filters = new List<FilterDefinition<BsonDocument>>();

                // If feilds are populated add conditions to the filter
                if (Validate.FeildPopulated(firstName)) filters.Add(filterBuilder.Eq("FirstName", firstName.ToLower().Replace(" ", string.Empty)));
                if (Validate.FeildPopulated(lastName)) filters.Add(filterBuilder.Eq("LastName", lastName.ToLower().Replace(" ", string.Empty)));
                if (Validate.FeildPopulated(salutationName)) filters.Add(filterBuilder.Eq("SalutionName", salutationName.ToLower().Replace("", string.Empty)));

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
            else
            {
                return null;
            }

        }



        //find single member (mainly for display purposes on 'select member' page)
        public static Member Find(ObjectId id)
        {
            //get DB
            var database = DatabaseConnection.GetDatabase();

            if (database != null)
            {
                // Get member collection
                var collection = database.GetCollection<BsonDocument>("Members");

                //get the document in the collection with that id
                var memberResult = collection.Find(new BsonDocument("_id", id));

                //deserialise into "member" format
                var memberObjectResult = BsonSerializer.Deserialize<Member>((BsonDocument)memberResult);

                return memberObjectResult;
            }
            else
            {
                return null;
            }
        }

    }
}

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
        //search sponsorship using just one member ID
        public static List<Sponsorship> FindByMember(ObjectId id)
        {
            //get DB
            var database = DatabaseConnection.GetDatabase();

            // Get sponsorship collection
            var collection = database.GetCollection<BsonDocument>("Sponsorships");

            //filter the collection
            var filter = Builders<BsonDocument>.Filter.Eq("Member_id", id);

            try
            {
                // Search sponsorship collection 
                List<BsonDocument> sponsorshipResults = collection.Find(filter).ToList();

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


        //search sponsorship using just one wingspan ID
        public static List<Sponsorship> FindByBird(string wingspanId)
        {
            //get DB
            var database = DatabaseConnection.GetDatabase();

            // Get sponsorship collection
            var collection = database.GetCollection<BsonDocument>("Sponsorships");

            //filter the collection
            var filter = Builders<BsonDocument>.Filter.Eq("WingspanId", wingspanId);

            try
            {
                // Search sponsorship collection 
                List<BsonDocument> sponsorshipResults = collection.Find(filter).ToList();

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


       

        //NO LONGER POSSIBLE
        //public static List<Sponsorship> Search(string wingspanID, ObjectId member)
        //{
        //    // Get DB
        //    var database = DatabaseConnection.GetDatabase();

        //    // Get sponsorship collection
        //    var collection = database.GetCollection<BsonDocument>("Sponsorships");

        //    // Used to build filter with multiple conditions
        //    var filterBuilder = Builders<BsonDocument>.Filter;

        //    List<FilterDefinition<BsonDocument>> filters = new List<FilterDefinition<BsonDocument>>();

        //    // If feilds are populated add conditions to the filter
        //    if (Validate.FeildPopulated(wingspanID)) filters.Add(filterBuilder.Eq("WingspanId", wingspanID));
        //    filters.Add(filterBuilder.Eq("Member_id", member));

        //    FilterDefinition<BsonDocument> searchFilter = filters[0];

        //    if (filters.Count > 1)
        //    {
        //        for (int i = 1; i < filters.Count; i++)
        //        {
        //            searchFilter |= filters[i];
        //        }
        //    }

        //    try
        //    {
        //        // Search sponsorship collection 
        //        List<BsonDocument> sponsorshipResults = collection.Find(searchFilter).ToList();

        //        // Convert results to sponsorship results
        //        List<Sponsorship> sponsorshipObjectResults = new List<Sponsorship>();
        //        foreach (var result in sponsorshipResults)
        //        {
        //            sponsorshipObjectResults.Add(BsonSerializer.Deserialize<Sponsorship>(result));
        //        }

        //        return sponsorshipObjectResults;
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //}
    }
}

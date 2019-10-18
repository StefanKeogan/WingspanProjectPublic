using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using WingspanPrototype1.Functions;
using WingspanPrototype1.Model;

namespace WingspanPrototype1.Controller.Volunteers
{
    class SearchVolunteers
    {
        public static List<Volunteer> Search(string volunteerEmail, string volunteerFirstName, string volunteerLastName)
        {
            // Get DB
            var database = DatabaseConnection.GetDatabase();

            // Get member collection
            var collection = database.GetCollection<BsonDocument>("Volunteers");

            // Used to build filter with multiple conditions
            var filterBuilder = Builders<BsonDocument>.Filter;

            // Build search filter
            List<FilterDefinition<BsonDocument>> filters = new List<FilterDefinition<BsonDocument>>();

            if (Validate.FeildPopulated(volunteerEmail)) filters.Add(filterBuilder.Eq("Email", volunteerEmail));           
            if (Validate.FeildPopulated(volunteerFirstName)) filters.Add(filterBuilder.Eq("FirstName", volunteerFirstName));
            if (Validate.FeildPopulated(volunteerLastName)) filters.Add(filterBuilder.Eq("LastName", volunteerLastName));

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
                // Search volunteers
                List<BsonDocument> volunteerResults = collection.Find(searchFilter).ToList();

                // TODO: Check list size 

                // Convert to volunteer objects 
                List<Volunteer> volunteerObjectResults = new List<Volunteer>();
                foreach (var result in volunteerResults)
                {
                    volunteerObjectResults.Add(BsonSerializer.Deserialize<Volunteer>(result));
                }

                return volunteerObjectResults;
            }
            catch (Exception)
            {
                return null;
            }
          
            
        }

    }

}

using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using WingspanPrototype1.Model;

namespace WingspanPrototype1.Controller.Volunteers
{
    class SearchVolunteers
    {
        public static List<Volunteer> Search(string volunteerEmail, string volunteerName)
        {
            // Get DB
            var database = DatabaseConnection.GetDatabase();

            // Get member collection
            var collection = database.GetCollection<Volunteer>("Members");

            // Used to build filter with multiple conditions
            var filterBuilder = Builders<Volunteer>.Filter;

            // Build search filter

            FilterDefinition<Volunteer> filter = null;

            if (volunteerEmail != string.Empty)
            {
                filter = filter | filterBuilder.Eq(volunteer => volunteer.Email, volunteerEmail);
            }

            if (volunteerName != string.Empty)
            {
                filter = filter | filterBuilder.Eq(member => member.Name, volunteerName);
            }

            // Search member collection 
            List<Volunteer> memberResults = collection.Find(filter).ToList();

            return memberResults;
        }

    }
}

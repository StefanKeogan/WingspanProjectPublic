using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;
using WingspanPrototype1.Functions;
using WingspanPrototype1.Model;

namespace WingspanPrototype1.Controller.Birds
{
    class AddMember
    {
        public static bool InsertMemberDocument(Member member)
        {
            // Get database
            DatabaseConnection databaseConnection = new DatabaseConnection();
            var database = databaseConnection.GetDatabase();

            if (database != null)
            {
                // Get collection 
                var collection = database.GetCollection<BsonDocument>("Members");

                // Create document object, add default values 
                var document = new BsonDocument
                {
                    {"JoinDate", member.JoinDate.ToLocalTime() },
                };

                // Add feilds that are populated 
                if (Validate.FeildPopulated(member.FirstName)) document.Add("FirstName", member.FirstName.Replace(" ", string.Empty).ToLower());
                if (Validate.FeildPopulated(member.LastName)) document.Add("LastName", member.LastName.Replace(" ", string.Empty).ToLower());
                if (Validate.FeildPopulated(member.SalutationName)) document.Add("SalutationName", member.SalutationName.Replace(" ", string.Empty).ToLower());
                if (Validate.FeildPopulated(member.Company)) document.Add("Company", member.Company.Replace(" ", string.Empty).ToLower());
                if (Validate.FeildPopulated(member.Email)) document.Add("Email", member.Email);
                if (Validate.FeildPopulated(member.Address1)) document.Add("Address1", member.Address1);
                if (Validate.FeildPopulated(member.Address2)) document.Add("Address2", member.Address2);
                if (Validate.FeildPopulated(member.Address3)) document.Add("Address3", member.Address3);
                if (Validate.FeildPopulated(member.City)) document.Add("City", member.City);
                if (Validate.FeildPopulated(member.Country)) document.Add("Country", member.Country);
                if (Validate.FeildPopulated(member.Postcode)) document.Add("Postcode", member.Postcode);
                if (Validate.FeildPopulated(member.Comment)) document.Add("Comment", member.Comment);

                // Insert document
                try
                {
                    collection.InsertOne(document);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }          
            
        }
    }
}

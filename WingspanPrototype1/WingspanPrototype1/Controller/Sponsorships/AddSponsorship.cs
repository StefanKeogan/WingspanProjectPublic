using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using WingspanPrototype1.Functions;
using WingspanPrototype1.Model;

namespace WingspanPrototype1.Controller.Sponsorships
{
    class AddSponsorship
    {
        public static bool InsertSponsorshipDocument(Sponsorship sponsorship)
        {
            // Create database 
            var database = DatabaseConnection.GetDatabase();

            if (database != null)
            {
                // Get collection 
                var collection = database.GetCollection<BsonDocument>("Sponsorship");

                // Create document object, add default values 
                var document = new BsonDocument
                {
                    { "Level", sponsorship.Category },
                    { "StartDate", sponsorship.SponsorshipStart },
                    { "EndDate", sponsorship.SponsorshipEnd },
                    { "Member_id", sponsorship.Member_id }
                };

                // Add feilds that are populated
                if (Validate.FeildPopulated(sponsorship.WingspanId)) document.Add("WingspanId", sponsorship.WingspanId);
                if (Validate.FeildPopulated(sponsorship.FirstName)) document.Add("FirstName", sponsorship.FirstName.Replace(" ", string.Empty).ToLower());
                if (Validate.FeildPopulated(sponsorship.LastName)) document.Add("LastName", sponsorship.LastName.Replace(" ", string.Empty).ToLower());
                if (Validate.FeildPopulated(sponsorship.Company)) document.Add("Company", sponsorship.Company.Replace(" ", string.Empty).ToLower());
                if (Validate.FeildPopulated(sponsorship.SponsorshipNotes)) document.Add("Notes", sponsorship.SponsorshipNotes);

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

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

            // Get collection 
            var collection = database.GetCollection<BsonDocument>("Sponsorship");

            //TO DO
            //-need to figure out how to get Wingspan ID from selection method
            //-what about "other"?
            //-need to figure out the sponsor name stuff too....


            // Create document object, add default values 
            var document = new BsonDocument
            {
                { "Level", sponsorship.SponsorshipCategory },
                { "StartDate", sponsorship.SponsorshipStart },
                { "EndDate", sponsorship.SponsorshipEnd }
            };

            // Add feilds that are populated 
            if (Validate.FeildPopulated(sponsorship.SponsorshipNotes)) document.Add("Notes", sponsorship.SponsorshipNotes);
            //if (Validate.FeildPopulated(sponsorship.FirstName)) document.Add("FirstName", sponsorship.FirstName);
            //if (Validate.FeildPopulated(sponsorship.LastName)) document.Add("LastName", sponsorship.LastName);
            //if (Validate.FeildPopulated(sponsorship.Company)) document.Add("Company", sponsorship.Company);

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
    }
}

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
                var collection = database.GetCollection<BsonDocument>("Sponsorships");

                // Create document object, add default values 
                var document = new BsonDocument
                {
                    { "Level", sponsorship.Category },
                    { "StartDate", sponsorship.StartDate },
                    { "EndDate", sponsorship.EndDate },
                    { "Member_id", sponsorship.Member_id }
                };

                // Add feilds that are populated
                if (Validate.FeildPopulated(sponsorship.WingspanId)) document.Add("WingspanId", sponsorship.WingspanId);
                if (Validate.FeildPopulated(sponsorship.Notes)) document.Add("Notes", sponsorship.Notes);

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

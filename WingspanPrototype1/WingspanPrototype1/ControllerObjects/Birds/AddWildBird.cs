using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using WingspanPrototype1.Model;

namespace WingspanPrototype1.Controller.Birds
{
    class AddWildBird
    {
        public static bool InsertWildBirdDocumnet(WildBird bird)
        {
            // Get database
            DatabaseConnection databaseConnection = new DatabaseConnection();
            var database = databaseConnection.GetDatabase();

            if (database != null)
            {
                var collection = database.GetCollection<BsonDocument>("WildBirds");

                // Insert auto generated / default feilds 
                var document = new BsonDocument
                {
                    {"WingspanId", bird.WingspanId}
                };

                // Further validation for un-required feilds 
                if (bird.Name != null) document.Add("Name", bird.Name);
                if (bird.Species != null) document.Add("Species", bird.Species);
                if (bird.Location != null) document.Add("Location", bird.Location.Replace(" ", string.Empty));
                if (bird.Age != null) document.Add("Age", bird.Age);
                if (bird.Sex != null) document.Add("Sex", bird.Sex);
                if (bird.MetalBand != null) document.Add("MetalBand", bird.MetalBand.Replace(" ", string.Empty));
                if (bird.BandInfo != null) document.Add("BandInfo", bird.BandInfo);
                if (bird.Gps != null) document.Add("Gps", bird.Gps.Replace(" ", string.Empty));
                if (bird.BanderName != null) document.Add("BanderName", bird.BanderName.Replace(" ", string.Empty));
                if (bird.DateBanded.ToString() != "1/01/0001 12:00:00 AM") document.Add("DateBanded", bird.DateBanded.ToLocalTime());

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

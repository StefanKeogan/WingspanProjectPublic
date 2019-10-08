using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace WingspanPrototype1.Controller.Birds
{
    class AddWildBird
    {
        public static bool InsertWildBirdDocumnet(WildBird bird)
        {
            var database = DatabaseConnection.GetDatabase();

            var collection = database.GetCollection<BsonDocument>("WildBirds");

            // Insert auto generated / default feilds 
            var document = new BsonDocument
                {
                    {"WingspanId", bird.WingspanId},
                    {"DateBanded", bird.DateBanded}
                };

            // Further validation for un-required feilds 
            if (bird.Species != null) document.Add("Species", bird.Species);
            if (bird.Location != null) document.Add("Location", bird.Location);
            if (bird.Age != null) document.Add("Age", bird.Age);
            if (bird.Sex != null) document.Add("Sex", bird.Sex);
            if (bird.MetalBand != null) document.Add("MetalBand", bird.MetalBand);
            if (bird.BandInfo != null) document.Add("BandInfo", bird.BandInfo);
            if (bird.Gps != null) document.Add("Gps", bird.Gps);
            if (bird.BanderName != null) document.Add("BanderName", bird.BanderName);

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

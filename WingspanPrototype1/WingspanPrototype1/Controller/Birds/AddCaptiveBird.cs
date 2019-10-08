using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;
using WingspanPrototype1.Model;

namespace WingspanPrototype1.Controller.Birds
{
    class AddCaptiveBird
    {
        public static bool InsertCaptiveBirdDocument(CaptiveBird bird)

        {
            var database = DatabaseConnection.GetDatabase();

            var collection = database.GetCollection<BsonDocument>("CaptiveBirds");

            // Insert auto generated / default feilds 
            var document = new BsonDocument
                {
                    {"WingspanId", bird.WingspanId},
                    {"DateArrived", bird.DateArrived}
                };

            // Further validation for un-required feilds 
            if (bird.Name != null) document.Add("Name", bird.Name);
            if (bird.BandNo != null) document.Add("BandNo", bird.BandNo);
            if (bird.BandColour != null) document.Add("BandColour", bird.BandColour);
            if (bird.Species != null) document.Add("Species", bird.Species);
            if (bird.Sex != null) document.Add("Sex", bird.Sex);
            if (bird.Age != null) document.Add("Age", bird.Age);
            if (bird.Location != null) document.Add("Location", bird.Location);

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

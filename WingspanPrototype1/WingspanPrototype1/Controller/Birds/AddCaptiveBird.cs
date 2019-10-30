using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
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

            if (database != null)
            {
                var collection = database.GetCollection<BsonDocument>("CaptiveBirds");

                // Insert auto generated / default feilds 
                var document = new BsonDocument
                {
                    {"WingspanId", bird.WingspanId},
                    {"DateArrived", bird.DateArrived.ToLocalTime()}
                };

                // Further validation for un-required feilds
                if (bird.Name != null) document.Add("Name", bird.Name.Replace(" ", string.Empty).ToLower());
                if (bird.BandNo != null) document.Add("BandNo", bird.BandNo.Replace(" ", string.Empty));
                if (bird.BandInfo != null) document.Add("BandInfo", bird.BandInfo.Replace(" ", string.Empty));
                if (bird.Species != null) document.Add("Species", bird.Species);
                if (bird.Sex != null) document.Add("Sex", bird.Sex);
                if (bird.Age != null) document.Add("Age", bird.Age);
                if (bird.Location != null) document.Add("Location", bird.Location.Replace(" ", string.Empty));

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

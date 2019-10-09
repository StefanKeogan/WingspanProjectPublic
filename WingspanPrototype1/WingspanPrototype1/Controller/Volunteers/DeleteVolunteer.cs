using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using WingspanPrototype1.Functions;

namespace WingspanPrototype1.Controller.Volunteers
{
    class DeleteVolunteer
    {
        public static bool DropDocument(ObjectId id)
        {
            var database = DatabaseConnection.GetDatabase();

            var collection = database.GetCollection<BsonDocument>("Volunteers");

            var filter = Builders<BsonDocument>.Filter.Eq("_id", id);

            try
            {
                collection.DeleteOne(filter);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}

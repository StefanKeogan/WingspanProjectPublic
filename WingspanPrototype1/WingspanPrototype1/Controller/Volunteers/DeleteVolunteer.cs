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
            // Get databse 
            var database = DatabaseConnection.GetDatabase();

            if (database != null)
            {
                // Get collection
                var collection = database.GetCollection<BsonDocument>("Volunteers");

                // Get filter 
                var filter = Builders<BsonDocument>.Filter.Eq("_id", id);

                // Delete document 
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
            else
            {
                return false;
            }           

        }
    }
}

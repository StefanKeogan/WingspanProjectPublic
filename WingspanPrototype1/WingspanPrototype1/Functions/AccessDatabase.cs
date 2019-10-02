using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace WingspanPrototype1
{
    class DatabaseConnection
    {
        public static IMongoDatabase GetDatabase()
        {
            // var client = new MongoClient("mongodb+srv://Toi19:Toi19@cluster0-mzn8m.mongodb.net/test?retryWrites=true&w=majority");

            var client = new MongoClient("mongodb://localhost:27017");

            var database = client.GetDatabase("WingspanDB");

            return database;

        }

    }


}

using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace WingspanPrototype1
{
    class AccessDatabase
    {
        public IMongoCollection<BsonDocument> collection;
        public AccessDatabase()
        {
            var client = new MongoClient("mongodb+srv://Toi19:Toi19@cluster0-mzn8m.mongodb.net/test?retryWrites=true&w=majority");
            var database = client.GetDatabase("test");

            collection = database.GetCollection<BsonDocument>("WingspanTest");
        }

        public List<BsonDocument> SearchCollection(string key, string value)
        {
            List<BsonDocument> results;
            return results = collection.Find(new BsonDocument(key, value)).ToList();
        }

    }


}

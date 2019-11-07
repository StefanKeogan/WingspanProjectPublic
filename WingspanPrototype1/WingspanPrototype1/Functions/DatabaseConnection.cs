using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace WingspanPrototype1
{
    class DatabaseConnection
    {
        public static IMongoDatabase GetDatabase()
        {
            // Wingspan Scale Grid Connection 
            //try
            //{
            //    string connectionString = "mongodb://birdbook:BirdBookIsAwesome19@SG-WingspanDB-27577.servers.mongodirector.com:50454,SG-WingspanDB-27578.servers.mongodirector.com:50454,SG-WingspanDB-27579.servers.mongodirector.com:50454/WingspanDB?replicaSet=RS-WingspanDB-0&ssl=true";

            //    var url = new MongoUrl(connectionString);

            //    var clientSettings = MongoClientSettings.FromUrl(url);

            //    clientSettings.SslSettings = new SslSettings();
            //    clientSettings.SslSettings.CheckCertificateRevocation = false;
            //    clientSettings.UseSsl = true;
            //    clientSettings.VerifySslCertificate = false;

            //    var client = new MongoClient(clientSettings);

            //    var database = client.GetDatabase("WingspanDB");
            //    return database;
            //}
            //catch (Exception)
            //{
            //    throw;
            //    // return null;
            //}


            // Local host connection
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("WingspanDB");
            return database;


        }

    }

}

using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Net;
using WingspanPrototype1.Functions;
using Xamarin.Forms;

namespace WingspanPrototype1
{
    class DatabaseConnection
    {
        // Local host connection string 
        // protected string connectionString = "mongodb://localhost:27017";

        // Dedicated Scale grid test connections string
        // protected string connectionString = "mongodb://birdbook:BirdBookIsAwesome19@SG-DedicatedTest-27835.servers.mongodirector.com:27017/WingspanDB";

        // Wingspan Database connection string 
        // protected string connectionString = "mongodb://birdbook:BirdBookIsAwesome19@SG-WingspanDB-27577.servers.mongodirector.com:50454,SG-WingspanDB-27578.servers.mongodirector.com:50454,SG-WingspanDB-27579.servers.mongodirector.com:50454/WingspanDB?replicaSet=RS-WingspanDB-0&ssl=true";

        // Clever cloud connection 
        protected string connectionString = "mongodb://uf5r4cvqwjrfnced0id7:KyixXt51y8XZVmLyd8n6@boeuzmf4hwi4iiw-mongodb.services.clever-cloud.com:27017/boeuzmf4hwi4iiw";

        protected MongoClient client;
        protected IMongoDatabase database;

        public IMongoDatabase GetDatabase()
        {
            // Wingspan Scale Grid Connection 
            try
            {
                var clientSettings = MongoClientSettings.FromConnectionString(connectionString);

                // SSL Settings only needed for Wingspan Database connection 
                //clientSettings.SslSettings = new SslSettings();
                //clientSettings.SslSettings.CheckCertificateRevocation = false;
                //clientSettings.UseSsl = true;
                //clientSettings.VerifySslCertificate = false;

                client = new MongoClient(clientSettings);

                // Used for all connections other than clever cloud 
                // database = client.GetDatabase("WingspanDB");
                
                // Used for clever cloud connection
                database = client.GetDatabase("boeuzmf4hwi4iiw");

                return database;
            }
            catch (Exception)
            {
                // throw;
                return null;
            }

        }

    }

}

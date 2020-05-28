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
                //clientSettings.UseTls = true;
                //clientSettings.AllowInsecureTls = true;

                client = new MongoClient(clientSettings);

                // Used for all connections other than clever cloud 
                // database = client.GetDatabase("WingspanDB");
                
                // Used for clever cloud connection
                database = client.GetDatabase("boeuzmf4hwi4iiw");

                return database;
            }
            catch (Exception)
            {
                return null;
            }

        }

    }

}

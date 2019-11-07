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
            // Clever Coud db connection
            //try
            //{
            //    var client = new MongoClient("mongodb://uf5r4cvqwjrfnced0id7:KyixXt51y8XZVmLyd8n6@boeuzmf4hwi4iiw-mongodb.services.clever-cloud.com:27017/boeuzmf4hwi4iiw");
            //    var database = client.GetDatabase("boeuzmf4hwi4iiw");
            //    return database;
            //}
            //catch (Exception)
            //{

            //    return null;
            //}

            // Scale Grid db connection
            string connectionString = "mongodb://wingspanapp:shadowfalconcricketmorepork@SG-WingspanDB-27502.servers.mongodirector.com:50432,SG-WingspanDB-27503.servers.mongodirector.com:50432,SG-WingspanDB-27504.servers.mongodirector.com:50432/WingspanDB?replicaSet=RS-WingspanDB-0&ssl=true";

            var url = new MongoUrl(connectionString);

            var clientSettings = MongoClientSettings.FromUrl(url);

            clientSettings.SslSettings = new SslSettings();
            clientSettings.SslSettings.CheckCertificateRevocation = false;
            clientSettings.UseSsl = true;
            clientSettings.VerifySslCertificate = false;

            var client = new MongoClient(clientSettings);
        
            var database = client.GetDatabase("WingspanDB");
            return database;

            // Local host connection
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("WingspanDB");
            return database;


        }

    }

}

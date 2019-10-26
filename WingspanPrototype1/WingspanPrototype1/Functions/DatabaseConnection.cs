﻿using MongoDB.Bson;
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
            // Clever Coud db connection
            var client = new MongoClient("mongodb://uf5r4cvqwjrfnced0id7:KyixXt51y8XZVmLyd8n6@boeuzmf4hwi4iiw-mongodb.services.clever-cloud.com:27017/boeuzmf4hwi4iiw");
            var database = client.GetDatabase("boeuzmf4hwi4iiw");

            // Scale Grid db connection
            //var client = new MongoClient("mongodb://admin:Onm5J5hQwbYE6Auz@SG-WingspanTest-26707.servers.mongodirector.com:50199/admin?replicaSet=RS-WingspanTest-0&ssl=true");
            //var database = client.GetDatabase("WingspanTestDB");

            // Local host connection
            //var client = new MongoClient("mongodb://localhost:27017");
            //var database = client.GetDatabase("WingspanDB");

            return database;

        }

    }


}

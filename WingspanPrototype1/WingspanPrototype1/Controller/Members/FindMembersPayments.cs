﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using WingspanPrototype1.Functions;
using WingspanPrototype1.Model;

namespace WingspanPrototype1.Controller.Members
{
    class FindMembersPayments
    {
        public static List<Payment> GetPaymentDocuments(ObjectId memberId)
        {
            // Get DB
            var database = DatabaseConnection.GetDatabase();

            // Get payment collection
            var collection = database.GetCollection<BsonDocument>("Payments");

            try
            {
                // Search payment collection 
                List<BsonDocument> paymentResults = collection.Find(Builders<BsonDocument>.Filter.Eq("Member_id", memberId)).ToList();

                // Convert results to member results
                List<Payment> paymentObjectResults = new List<Payment>();
                foreach (var result in paymentResults)
                {
                    paymentObjectResults.Add(BsonSerializer.Deserialize<Payment>(result));
                }
                return paymentObjectResults;
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}

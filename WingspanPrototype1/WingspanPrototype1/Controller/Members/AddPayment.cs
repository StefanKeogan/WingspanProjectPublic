using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;
using WingspanPrototype1.Model;
using WingspanPrototype1.Functions;

namespace WingspanPrototype1.Controller.Members
{
    class AddPayment
    {
        public static bool InsertPaymentDocument(ObjectId memberId, Payment payment)
        {
            // Get database
            var database = DatabaseConnection.GetDatabase();

            if (database != null)
            {
                // Get payment collection
                var collection = database.GetCollection<BsonDocument>("Payments");

                // Create payment document
                var document = new BsonDocument
                {
                    {"Date", payment.Date.ToLocalTime() },
                    {"Amount", payment.Amount },
                    {"Member_id", memberId }
                };

                try
                {
                    // Insert document
                    collection.InsertOne(document);
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

﻿using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;
using WingspanPrototype1.Functions;
using WingspanPrototype1.Model;

namespace WingspanPrototype1.Controller.Volunteers
{
    class AddVolunteer
    {
        public static bool InsertVolunteerDocument(Volunteer volunteer)
        {
            // Get database
            DatabaseConnection databaseConnection = new DatabaseConnection();
            var database = databaseConnection.GetDatabase();

            if (database != null)
            {
                // Get collection
                var collection = database.GetCollection<BsonDocument>("Volunteers");

                // Insert required feilds 
                var document = new BsonDocument
            {
                {"FirstName", volunteer.FirstName.Replace(" ", string.Empty).ToLower() }
            };

                // What other feilds do we need to insert?
                if (Validate.FeildPopulated(volunteer.LastName)) document.Add("LastName", volunteer.LastName.Replace(" ", string.Empty).ToLower());
                if (Validate.FeildPopulated(volunteer.Email)) document.Add("Email", volunteer.Email.Replace(" ", string.Empty));
                if (Validate.FeildPopulated(volunteer.Mobile.ToString()))
                {
                    if (volunteer.Mobile != 0)
                    {
                        document.Add("Mobile", volunteer.Mobile);
                    }
                } 

                try
                {
                    // Insert volunteer collection
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

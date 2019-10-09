﻿using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;
using WingspanPrototype1.Functions;
using WingspanPrototype1.Model;

namespace WingspanPrototype1.Controller.Birds
{
    class AddMember
    {
        public static bool InsertMemberDocument(Member member)
        {
            var database = DatabaseConnection.GetDatabase();

            var collection = database.GetCollection<BsonDocument>("Members");

            var document = new BsonDocument
                {
                    {"JoinDate", member.JoinDate },
                };

            if (Validate.FeildPopulated(member.FirstName)) document.Add("FirstName", member.FirstName);
            if (Validate.FeildPopulated(member.LastName)) document.Add("LastName", member.LastName);
            if (Validate.FeildPopulated(member.Email)) document.Add("Email", member.Email);
            if (Validate.FeildPopulated(member.Address1)) document.Add("Address1", member.Address1);
            if (Validate.FeildPopulated(member.Address2)) document.Add("Address2", member.Address2);
            if (Validate.FeildPopulated(member.Address3)) document.Add("Address3", member.Address3);
            if (Validate.FeildPopulated(member.City)) document.Add("City", member.City);
            if (Validate.FeildPopulated(member.Postcode)) document.Add("Postcode", member.Postcode);
            if (Validate.FeildPopulated(member.Comment)) document.Add("Comment", member.Comment);

            try
            {               
                collection.InsertOne(document);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
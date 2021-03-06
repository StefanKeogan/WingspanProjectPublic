﻿using System;
using System.Collections.Generic;
using System.Text;
using WingspanPrototype1.Model;
using WingspanPrototype1.Functions;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Bson.Serialization;
using WingspanPrototype1.Controller.Birds;

namespace WingspanPrototype1.Controller.Sponsorships
{
    class SearchSponsorships
    {
        //search sponsorship using just one member ID
        public static List<Sponsorship> FindByMember(ObjectId id)
        {
            // Get database
            DatabaseConnection databaseConnection = new DatabaseConnection();
            var database = databaseConnection.GetDatabase();

            // Get sponsorship collection
            var collection = database.GetCollection<BsonDocument>("Sponsorships");

            //filter the collection
            var filter = Builders<BsonDocument>.Filter.Eq("Member_id", id);

            try
            {
                // Search sponsorship collection 
                List<BsonDocument> sponsorshipResults = collection.Find(filter).ToList();

                // Convert results to sponsorship results
                List<Sponsorship> sponsorshipObjectResults = new List<Sponsorship>();
                foreach (var result in sponsorshipResults)
                {
                    sponsorshipObjectResults.Add(BsonSerializer.Deserialize<Sponsorship>(result));
                }
                return sponsorshipObjectResults;
            }
            catch (Exception)
            {
                return null;
            }
        }


        //search sponsorship using just one wingspan ID
        public static List<Sponsorship> FindByBird(string wingspanId)
        {
            // Get database
            DatabaseConnection databaseConnection = new DatabaseConnection();
            var database = databaseConnection.GetDatabase();

            // Get sponsorship collection
            var collection = database.GetCollection<BsonDocument>("Sponsorships");

            //filter the collection
            var filter = Builders<BsonDocument>.Filter.Eq("WingspanId", wingspanId);

            try
            {
                // Search sponsorship collection 
                List<BsonDocument> sponsorshipResults = collection.Find(filter).ToList();

                // Convert results to sponsorship results
                List<Sponsorship> sponsorshipObjectResults = new List<Sponsorship>();
                foreach (var result in sponsorshipResults)
                {
                    sponsorshipObjectResults.Add(BsonSerializer.Deserialize<Sponsorship>(result));
                }

                return sponsorshipObjectResults;

            }
            catch (Exception)
            {
                return null;
            }

        }

        public static List<Sponsorship> SearchByMember(string memberFirstName, string memberLastName, string memberCompanyName)
        {
            // Store found sponsorships
            List<Sponsorship> sponsorshipResults = new List<Sponsorship>();

            // First get possible members 
            List<Member> possibleMembers = SearchMembers.Search(memberFirstName, memberLastName, memberCompanyName);

            // If members found 
            if ((possibleMembers != null) && (possibleMembers.Count > 0))
            {
                // Get database
                DatabaseConnection databaseConnection = new DatabaseConnection();
                var database = databaseConnection.GetDatabase();

                if (database != null)
                { 

                    // Get sponsorship collection 
                    var collection = database.GetCollection<BsonDocument>("Sponsorships");

                    foreach (var member in possibleMembers)
                    {
                        // Build filter for this member 
                        var filter = Builders<BsonDocument>.Filter.Eq("Member_id", member.Member_id);

                        try
                        {
                            // Search for member
                            List<BsonDocument> results = collection.Find(filter).ToList();
                            if ((results != null) && (results.Count > 0))
                            {
                                foreach (var result in results)
                                {
                                    sponsorshipResults.Add(BsonSerializer.Deserialize<Sponsorship>(result));
                                }
                            }
                            else
                            {
                                continue;
                            }
                        }
                        catch (Exception)
                        {
                            continue;
                        }

                    }

                    return sponsorshipResults;

                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }

        }
        
    }
}

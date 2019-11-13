using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using WingspanPrototype1.Model;

namespace WingspanPrototype1.Controller.Birds
{
    class FindBirdNotes
    {
        public static List<Note> GetNoteDocuments(string wingspanId)
        {
            // Get DB
            var database = DatabaseConnection.GetDatabase();

            if (database != null)
            {
                // Get note collection
                var collection = database.GetCollection<BsonDocument>("NoteHistory");

                try
                {
                    // Search note collection 
                    List<BsonDocument> noteResults = collection.Find(Builders<BsonDocument>.Filter.Eq("WingspanId", wingspanId.ToLower())).ToList();

                    // Convert results to note results
                    List<Note> noteObjectResults = new List<Note>();
                    foreach (var result in noteResults)
                    {
                        noteObjectResults.Add(BsonSerializer.Deserialize<Note>(result));
                    }
                    return noteObjectResults;
                }
                catch (Exception)
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

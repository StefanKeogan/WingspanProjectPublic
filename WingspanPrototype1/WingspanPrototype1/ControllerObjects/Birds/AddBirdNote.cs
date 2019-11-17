using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;
using WingspanPrototype1.Model;

namespace WingspanPrototype1.Controller.Birds
{
    class AddBirdNote
    {
        public static bool InsertNoteDocument(Note note)
        {
            // Get database
            DatabaseConnection databaseConnection = new DatabaseConnection();
            var database = databaseConnection.GetDatabase();

            if (database != null)
            {
                // Get Note history collection
                var collection = database.GetCollection<BsonDocument>("NoteHistory");

                // Create document
                var document = new BsonDocument
                {
                    { "Date", note.Date.ToLocalTime() },
                    { "Category", note.Category },
                    { "Comment", note.Comment },
                    { "WingspanId", note.WingspanId.ToLower()}
                };

                // Insert docunment
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
            else
            {
                return false;
            }
           
        }
    }
}

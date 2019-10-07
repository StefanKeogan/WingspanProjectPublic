using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;
using WingspanPrototype1.Model;

namespace WingspanPrototype1.Controller.Birds
{
    class AddBirdNote
    {
        public static bool InsertBirdNote(Note note)
        {
            var database = DatabaseConnection.GetDatabase();

            var collection = database.GetCollection<BsonDocument>("NoteHistory");

            try
            {
                var document = new BsonDocument
                {
                    { "Date", note.Date },
                    { "Category", note.Category },
                    { "Comment", note.Comment },
                    { "WingspanId", note.Comment}
                };

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

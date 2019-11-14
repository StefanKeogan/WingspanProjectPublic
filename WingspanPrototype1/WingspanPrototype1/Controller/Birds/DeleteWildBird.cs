using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using WingspanPrototype1.Model;

namespace WingspanPrototype1.Controller.Birds
{
    class DeleteWildBird
    {
        public static bool DropDocument(string wingspanId)
        {
            // Get database
            var database = DatabaseConnection.GetDatabase();

            if (database != null)
            {
                // Get collection 
                var collection = database.GetCollection<BsonDocument>("WildBirds");

                // Crease filter 
                var filter = Builders<BsonDocument>.Filter.Eq("WingspanId", wingspanId);

                bool deleted = false;

                // Delete document 
                try
                {
                    collection.DeleteOne(filter);
                    deleted = true;
                }
                catch (Exception)
                {
                    return false;
                }

                // Was the bird successfully deleted?
                if (deleted)
                {
                    try
                    {
                        // Are there notes accociated with this bird
                        List<Note> notes = FindBirdNotes.GetNoteDocuments(wingspanId);
                        
                        // If notes are found, delete them
                        if ((notes != null) && (notes.Count > 0))
                        {
                            foreach (var note in notes)
                            {
                                DeleteBirdNote.DropDocument(note.Note_id);
                            }
                        }

                        // Return that bird has been deleted
                        return true;
                    }
                    catch (Exception)
                    {

                        return false;
                    }

                }
                else // If bird note deleted return false
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

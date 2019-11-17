using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using WingspanPrototype1.Model;

namespace WingspanPrototype1.Controller.Birds
{
    class DeleteCaptiveBird
    {
        public static bool DropDocument(string wingspanId)
        {
            // Get database
            DatabaseConnection databaseConnection = new DatabaseConnection();
            var database = databaseConnection.GetDatabase();

            if (database != null)
            {
                // Get Captive birds collection
                var collection = database.GetCollection<BsonDocument>("CaptiveBirds");

                // Build filter
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
                    try // Delete any database items accociated with that bird
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

                        List<Location> locations = FindBirdLocationHistory.GetLocationDocuments(wingspanId);

                        // If locations are found, delete them
                        if ((locations != null) && (locations.Count > 0))
                        {
                            foreach (var location in locations)
                            {
                                DeleteBirdLocation.DropDocument(location.Location_id);
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
                else
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

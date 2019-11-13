using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace WingspanPrototype1.Model
{
    public class Note
    {
        [BsonId]
        public ObjectId Note_id { get; set; }
        public DateTime Date { get; set; }
        public string Category { get; set; }
        public string Comment { get; set; }
        public string WingspanId { get; set; }
    }
}

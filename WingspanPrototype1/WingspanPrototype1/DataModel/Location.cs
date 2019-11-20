using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace WingspanPrototype1.Model
{
    public class Location
    {
        [BsonId]
        public ObjectId Location_id { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Date { get; set; }
        public string Category { get; set; }
        public string BirdLocation { get; set; }
        public string WingspanId { get; set; }
    }
}

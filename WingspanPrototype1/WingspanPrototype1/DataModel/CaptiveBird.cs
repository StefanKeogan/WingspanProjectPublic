using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace WingspanPrototype1.Model
{
    public class CaptiveBird
    {
        [BsonId]
        public ObjectId Captive_id { get; set; }
        public string WingspanId { get; set; }
        public string Name { get; set; }
        public string BandNo { get; set; }
        public string BandInfo { get; set; }
        public string Species { get; set; }
        public string Age { get; set; }
        public string Sex { get; set; }      
        public string Location { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime DateArrived { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime DateDeparted { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime DateDeceased { get; set; }
        public string Result { get; set; }
        
    }
}

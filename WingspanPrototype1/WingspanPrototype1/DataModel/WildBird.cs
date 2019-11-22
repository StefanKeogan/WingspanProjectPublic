using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace WingspanPrototype1
{
    public class WildBird
    {
        [BsonId]
        public ObjectId Wild_id { get; set; }
        public string WingspanId{ get; set; }
        public string Name { get; set; }
        public string Species { get; set; }
        public string Location { get; set; }
        public string Age { get; set; }
        public string Sex { get; set; }
        public string MetalBand { get; set; }
        public string BandInfo { get; set; }      
        public string Gps { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime DateBanded { get; set; }
        public string BanderName { get; set; }
    }
}

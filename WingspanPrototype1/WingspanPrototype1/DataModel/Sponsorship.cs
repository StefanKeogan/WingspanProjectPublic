using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
namespace WingspanPrototype1.Model
{
    public class Sponsorship
    {
        [BsonId]
        public ObjectId Sponsorship_id { get; set; }
        public string WingspanId { get; set; }
        public string Category { get; set; }
        public string Notes { get; set; }
        public ObjectId Member_id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace WingspanPrototype1.Model
{
    public class VolunteerHours
    {
        [BsonId]
        public ObjectId Hours_id { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Date { get; set; }
        public double Amount { get; set; }
        public string Note { get; set; }
        public ObjectId Volunteer_id { get; set; }
    }
}

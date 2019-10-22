using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace WingspanPrototype1.Model
{
    public class VolunteerHours
    {
        public ObjectId _id { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }
        public string Note { get; set; }
        public ObjectId Volunteer_id { get; set; }
    }
}

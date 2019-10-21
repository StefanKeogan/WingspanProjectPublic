using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace WingspanPrototype1.Model
{
    public class Location
    {
        public ObjectId _id { get; set; }
        public DateTime Date { get; set; }
        public string Category { get; set; }
        public string BirdLocation { get; set; }
        public string WingspanId { get; set; }
    }
}

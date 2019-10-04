using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace WingspanPrototype1
{
    public class WildBird
    {
        public ObjectId _id { get; set; }
        public string WingspanId{ get; set; }
        public string Species { get; set; }
        public string Location { get; set; }
        public string Age { get; set; }
        public string Sex { get; set; }
        public string Band { get; set; }
        public string BandInfo { get; set; }      
        public string Gps { get; set; }
        public string DateBanded { get; set; }
        public string BanderName { get; set; }
    }
}

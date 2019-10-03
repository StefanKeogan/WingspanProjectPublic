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
        public string WildSpecies { get; set; }
        public string WildLocation { get; set; }
        public string WildAge { get; set; }
        public string WildSex { get; set; }
        public string WildBand { get; set; }
        public string WildBandInfo { get; set; }      
        public string WildGps { get; set; }
        public string WildDateBanded { get; set; }
        public string WildBanderName { get; set; }
    }
}

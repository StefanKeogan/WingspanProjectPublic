using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace WingspanPrototype1.Model
{
    class Payment
    {
        [BsonId]
        public ObjectId Payment_id { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }
        public ObjectId Member_id { get; set; }

    }
}

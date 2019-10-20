using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace WingspanPrototype1.Model
{
    class Payment
    {
        public ObjectId _id { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }
        public ObjectId Member_id { get; set; }

    }
}

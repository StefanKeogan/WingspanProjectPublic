using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace WingspanPrototype1.Model
{
    class IdValue
    {
        public ObjectId _id {get; set;}

        public int Value { get; set; }
    }
}

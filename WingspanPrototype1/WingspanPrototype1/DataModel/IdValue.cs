using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace WingspanPrototype1.Model
{
    class IdValue
    {
        [BsonId]
        public ObjectId Value_id {get; set;}

        public int Value { get; set; }
    }
}

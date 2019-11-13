using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace WingspanPrototype1.Model
{
    public class Member
    {
        [BsonId]
        public ObjectId Member_id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SalutationName { get; set; }
        public string Email { get; set; }
        public string Company { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Postcode { get; set; }
        public string Comment { get; set; }
        public DateTime JoinDate { get; set; }


    }
}

using MongoDB.Bson;
using System;
namespace WingspanPrototype1.Model
{
    public class Sponsorship
    {
        public ObjectId _id { get; set; }
        public string WingspanId { get; set; }
        public string SponsorshipCategory { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SaluationName { get; set; }
        public string Company { get; set; }
        public DateTime SponsorshipStart { get; set; }
        public DateTime SponsorshipEnd { get; set; }
    }
}

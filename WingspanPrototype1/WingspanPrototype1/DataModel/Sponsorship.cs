using MongoDB.Bson;
using System;
namespace WingspanPrototype1.Model
{
    public class Sponsorship
    {
        public ObjectId _id { get; set; }
        public string WingspanId { get; set; }
        public string SponsorshipCategory { get; set; }
        public string SponsorID { get; set; }
        public string SponsorName { get; set; }
        public DateTime SponsorshipStart { get; set; }
        public DateTime SponsorshipEnd { get; set; }
    }
}

using MongoDB.Bson;
using System;
namespace WingspanPrototype1.Model
{
    public class Sponsorship
    {
        public ObjectId _id { get; set; }
        public string WingspanId { get; set; }
        public string Category { get; set; }
        public string SponsorshipNotes { get; set; }
        public ObjectId Member_id { get; set; }
        public DateTime SponsorshipStart { get; set; }
        public DateTime SponsorshipEnd { get; set; }
    }
}

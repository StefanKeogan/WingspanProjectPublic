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


        //these are needed because they are fields on the 'search sponsorship' page
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }

    }
}

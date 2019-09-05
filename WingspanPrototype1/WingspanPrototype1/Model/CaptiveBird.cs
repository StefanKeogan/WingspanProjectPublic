using System;
using System.Collections.Generic;
using System.Text;

namespace WingspanPrototype1.Model
{
    class CaptiveBird
    {
        public string WingspanId { get; set; }
        public string Name { get; set; }
        public string BandNo { get; set; }
        public string BandColour { get; set; }
        public string Species { get; set; }
        public string Age { get; set; }
        public string Sex { get; set; }      
        public string Location { get; set; }
        public DateTime DateArrived { get; set; }
        public DateTime DateDeparted { get; set; }
        public DateTime DateDeceased { get; set; }
        public string Result { get; set; }
        
    }
}

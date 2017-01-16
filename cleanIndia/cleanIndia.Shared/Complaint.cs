using System;
using System.Collections.Generic;
using System.Text;

namespace cleanIndia
{
    class Complaint
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string ComplaintName { get; set; }
        public DateTime DateOfComplaint { get; set; }
        public DateTime DateOfSchedule { get; set; }
        public DateTime DateOfCompletion { get; set; }
        public string ImageURI { get; set; }
        public string UserName { get; set; }
        public string AgencyName { get; set; }

    }
}

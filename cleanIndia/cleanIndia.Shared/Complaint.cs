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
        public string DateOfComplaint { get; set; }
        public string DateOfSchedule { get; set; }
        public string DateOfCompletion { get; set; }
        public string ComplaintImageURI { get; set; }
        public string FinalImageURI { get; set; }
        public string UserName { get; set; }
        public string AgencyName { get; set; }

    }
}

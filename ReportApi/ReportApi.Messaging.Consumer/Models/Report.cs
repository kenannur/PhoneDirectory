using System.Collections.Generic;

namespace ReportApi.Messaging.Consumer.Models
{
    public class Report
    {
        public List<string> LocationInformationFromMostToLeast { get; set; }

        public List<RegisteredPeopleInfo> NumberOfPeopleRegisteredAt { get; set; }

        public List<RegisteredPhoneInfo> NumberOfPhoneRegisteredAt { get; set; }
    }

    public class RegisteredPeopleInfo
    {
        public string Location { get; set; }

        public int Count { get; set; }
    }

    public class RegisteredPhoneInfo
    {
        public string Location { get; set; }

        public int Count { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace FunctionApp1.Models
{
    public class MessageToMom
    {
        public string From { get; set; }
        public DateTime? HowSoon { get; set; }
        public string Greeting { get; set; }
        public decimal HowMuch { get; set; }
        public List<string> Flattery { get; set; }
        public DateTime SubmittedDate { get; set; } //Added this field. This field should be added into the requirements.
    }
}

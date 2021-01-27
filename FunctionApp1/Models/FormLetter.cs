using System;

namespace FunctionApp1.Models
{
    public class FormLetter
    {
        public string Heading { get; set; }
        public double Likelihood { get; set; }
        public DateTime ExpectedDate { get; set; }
        public DateTime RequestedDate { get; set; }
        public string Body { get; set; }
    }
}

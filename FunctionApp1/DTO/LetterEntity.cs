using FunctionApp1.Models;
using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace FunctionApp1.DTO
{
    public class LetterEntity : TableEntity
    {
        public string Heading { get; set; }
        public double Likelihood { get; set; }
        public DateTime ExpectedDate { get; set; }
        public DateTime RequestedDate { get; set; }
        public string Body { get; set; }

        public LetterEntity()
        {

        }

        public LetterEntity(FormLetter formLetter)
        {
            Body = formLetter.Body;
            ExpectedDate = formLetter.ExpectedDate;
            Heading = formLetter.Heading;
            Likelihood = formLetter.Likelihood;
            RequestedDate = formLetter.RequestedDate;

            //Required Table fields
            RowKey = Guid.NewGuid().ToString();
            PartitionKey = $"{ExpectedDate:yyyy-MM}";
        }
    }
}

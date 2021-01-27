using FunctionApp1.Interfaces;
using FunctionApp1.Models;
using System;
using System.Globalization;

namespace FunctionApp1
{
    public class LetterComposer : ILetterComposer
    {

        private MessageToMom _messageToMom;
        private ILoanCalculator _loanProcessor;

        public LetterComposer(ILoanCalculator loanProcessor, MessageToMom messageToMom)
        {
            _messageToMom = messageToMom;
            _loanProcessor = loanProcessor;
             
        }

        public FormLetter Compose()
        {
            return new FormLetter()
            {
                //Parse flattery list into comma separated string
                //Populate Header with salutation comma separated string and "Mother"
                //Heading=Greeting
                Heading = new CultureInfo("en-us", false).TextInfo.ToTitleCase(string.Join(", ", _messageToMom.Flattery)) +
                    " Mother, " + _messageToMom.Greeting,

                //Body:"Really need help: I need $5523.23 by December 12,2020"
                Body = $"Really need help: I need {_messageToMom.HowMuch:c} by {_messageToMom.HowSoon.Value:MMMM dd, yyyy}",

                //ExpectedDate = calculated date
                ExpectedDate = _loanProcessor.GetLoanReleaseDate(submissionDate: _messageToMom.SubmittedDate),

                //Likelihood = calculated likelihood
                Likelihood = _loanProcessor.GetLikelihood(_messageToMom.HowMuch),

                //RequestedDate = howsoon
                RequestedDate = _messageToMom.HowSoon.Value
            };
        }
    }
}

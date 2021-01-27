using FunctionApp1.Interfaces;
using System;

namespace FunctionApp1
{
    public class LoanCalculator : ILoanCalculator
    {
        private int _loanProcessingDays;
        private decimal _maxLoanableAmount;

        public int LoanProcessingDays
        {
            get { return _loanProcessingDays; }
            set { _loanProcessingDays = value; }
        }

        public decimal MaxLoanableAmount
        {
            get { return _maxLoanableAmount; }
            set { _maxLoanableAmount = value; }
        }

        public LoanCalculator()
        {

        }

        public LoanCalculator(int loanProcessingDays, decimal maxLoanableAmount)
        {
            _loanProcessingDays = loanProcessingDays;
            _maxLoanableAmount = maxLoanableAmount;
        }

        public DateTime GetLoanReleaseDate(DateTime submissionDate)
        {
            //Calculate approximate actual date of loan receipt based on this decision tree
            //Funds will be made available 10 business days after day of submission
            //Business days are weekdays, there are no holidays that are applicable
            DateTime nextDay = submissionDate;
            int i = 1;
            do
            {
                nextDay = nextDay.AddDays(1);
                if (nextDay.DayOfWeek == DayOfWeek.Saturday || nextDay.DayOfWeek == DayOfWeek.Sunday)
                    _loanProcessingDays++;
                
                i++;

            } while (i <= _loanProcessingDays);

            return nextDay;
        }

        public double GetLikelihood(decimal requestedAmount)
        {
            //Calculate likelihood of receiving loan based on this decision tree
            //100 percent likelihood (initial value) minus the probability expressed from the 
            //quotient of howmuch and the total maximum amount ($10000)
            return decimal.ToDouble(100 - requestedAmount / _maxLoanableAmount);
        }

    }
}

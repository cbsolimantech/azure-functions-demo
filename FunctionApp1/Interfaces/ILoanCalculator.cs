using System;

namespace FunctionApp1.Interfaces
{
    public interface ILoanCalculator
    {
        double GetLikelihood(decimal requestedAmount);
        DateTime GetLoanReleaseDate(DateTime submissionDate);
    }
}
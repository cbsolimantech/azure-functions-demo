using FunctionApp1.Interfaces;
using Moq;
using System;

namespace FunctionApp1.Tests.Mock
{
    public class LoanCalculatorMock
    {
        public ILoanCalculator LoanCalculator()
        { 
            var calculator = new Mock<ILoanCalculator>();

            calculator.Setup(p => p.GetLoanReleaseDate(It.IsAny<DateTime>())).Returns(() => default);
            calculator.Setup(p => p.GetLikelihood(It.IsAny<decimal>())).Returns(() => default);

            return calculator.Object;
        }
    }
}

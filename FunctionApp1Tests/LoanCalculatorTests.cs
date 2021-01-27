using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace FunctionApp1.Tests
{
    [TestClass()]
    public class LoanCalculatorTests
    {
        [TestMethod()]
        public void GetLoanReleaseDateTest()
        {
            //Arrange
            var submissionDate = new DateTime(2021, 1, 27);
            var numberOfProcessingDays = 10;
            var expected = new DateTime(2021, 2, 10);

            //Act
            var actual = new LoanCalculator() { LoanProcessingDays = numberOfProcessingDays }.GetLoanReleaseDate(submissionDate);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetLikelihoodTest()
        {
            //Arrange
            var maximumLoanableAmount = 10000;
            var requestedAmount = 1222.22m;
            var expected = 99.877778;

            //Act
            var actual = new LoanCalculator() { MaxLoanableAmount = maximumLoanableAmount }.GetLikelihood(requestedAmount);

            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
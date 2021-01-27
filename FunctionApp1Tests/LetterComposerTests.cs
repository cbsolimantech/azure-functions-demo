using FunctionApp1.Models;
using FunctionApp1.Tests.Mock;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace FunctionApp1.Tests
{
    [TestClass()]
    public class LetterComposerTests
    {
        [TestMethod()]
        public void ComposeTest()
        {

            //Arrange
            var messageToMom = new MessageToMom
            {
                From = "yourbelovedson@gmail.com",
                HowSoon = new DateTime(2021, 2, 1),
                Greeting = "So Good To Hear From You",
                HowMuch = 1222.22m,
                Flattery = new List<string> { "amazing", "fabulous", "profitable" },
                SubmittedDate = new DateTime(2021, 1, 27)
            };

            var expected = new FormLetter
            {
                Heading = "Amazing, Fabulous, Profitable Mother, So Good To Hear From You",
                Likelihood = default,
                ExpectedDate = default,
                RequestedDate = new DateTime(2021, 2, 1),
                Body = "Really need help: I need $1,222.22 by February 01, 2021"
            };

            //Act
            var actual = new LetterComposer(new LoanCalculatorMock().LoanCalculator(), messageToMom).Compose();

            //Assert           
            Assert.AreEqual(expected.Heading, actual.Heading);
            Assert.AreEqual(expected.Likelihood, actual.Likelihood);
            Assert.AreEqual(expected.ExpectedDate, actual.ExpectedDate);
            Assert.AreEqual(expected.RequestedDate, actual.RequestedDate);
            Assert.AreEqual(expected.Body, actual.Body);
        }
    }
}
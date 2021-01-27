using FunctionApp1.Models;
using FunctionApp1.Tests.Mock;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FunctionApp1.Tests
{
    [TestClass()]
    public class CalculateDatesAndAmountsFunctionTests
    {
        [TestMethod()]
        public async Task RunTest()
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
                Likelihood = 99.877778,
                ExpectedDate = new DateTime(2021, 2, 10),
                RequestedDate = new DateTime(2021, 2, 1),
                Body = "Really need help: I need $1,222.22 by February 01, 2021"
            };

            var collector = new CollectorMock<FormLetter>();

            //Act
            await new CalculateDatesAndAmountsFunction().Run(messageToMom, collector.Collector(), new LoggerMock().Logger());

            //Assert
            //Assert message has been added to Queue
            Assert.IsTrue(collector.Items.Count > 0);

            //Assert item added to queue is as expected
            FormLetter actual = collector.Items[0];
            Assert.AreEqual(expected.Heading, actual.Heading);
            Assert.AreEqual(expected.Likelihood, actual.Likelihood);
            Assert.AreEqual(expected.ExpectedDate, actual.ExpectedDate);
            Assert.AreEqual(expected.RequestedDate, actual.RequestedDate);
            Assert.AreEqual(expected.Body, actual.Body);
        }
    }
}
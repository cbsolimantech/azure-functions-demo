using FunctionApp1.DTO;
using FunctionApp1.Models;
using FunctionApp1.Tests.Mock;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace FunctionApp1.Tests
{
    [TestClass()]
    public class LogFormLetterToStorageTests
    {
        [TestMethod()]
        public async Task RunTest()
        {
            //Arrange
            var formLetter = new FormLetter
            {
                Heading = "Amazing, Fabulous, Profitable Mother, So Good To Hear From You",
                Likelihood = 99.877778,
                ExpectedDate = new DateTime(2021, 2, 10),
                RequestedDate = new DateTime(2021, 2, 1),
                Body = "Really need help: I need $1,222.22 by February 01, 2021"
            };

            var expected = new LetterEntity
            {
                Heading = "Amazing, Fabulous, Profitable Mother, So Good To Hear From You",
                Likelihood = 99.877778,
                ExpectedDate = new DateTime(2021, 2, 10),
                RequestedDate = new DateTime(2021, 2, 1),
                Body = "Really need help: I need $1,222.22 by February 01, 2021"
            };

            var collector = new CollectorMock<LetterEntity>();

            //Act
            await new LogFormLetterToStorage().Run(formLetter, collector.Collector(), new LoggerMock().Logger());

            //Assert
            //Assert message has been added to Queue
            Assert.IsTrue(collector.Items.Count > 0);

            //Assert item added to queue is as expected
            LetterEntity actual = collector.Items[0];
            Assert.AreEqual(expected.Heading, actual.Heading);
            Assert.AreEqual(expected.Likelihood, actual.Likelihood);
            Assert.AreEqual(expected.ExpectedDate, actual.ExpectedDate);
            Assert.AreEqual(expected.RequestedDate, actual.RequestedDate);
            Assert.AreEqual(expected.Body, actual.Body);
        }
    }
}
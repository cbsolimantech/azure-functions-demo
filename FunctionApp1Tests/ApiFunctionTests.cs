using FunctionApp1.Models;
using FunctionApp1.Tests.Mock;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FunctionApp1.Tests
{
    [TestClass()]
    public class ApiFunctionTests
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

            var query = new Dictionary<string, StringValues>();
            var body = JsonConvert.SerializeObject(messageToMom, Formatting.Indented);
            var collector = new CollectorMock<MessageToMom>();

            //Act
            var actual = (OkObjectResult)await new ApiFunction().Run(new HttpRequestMock().HttpRequest(query, body),
                                                                     collector.Collector(),
                                                                     new LoggerMock().Logger());

            //Assert message was processed successfully
            Assert.AreEqual(messageToMom.Greeting, actual.Value);

            //Assert message has been added to Queue
            Assert.IsTrue(collector.Items.Count > 0);
        }
    }
}
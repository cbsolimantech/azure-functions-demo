using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Primitives;
using Moq;
using System.Collections.Generic;
using System.IO;

namespace FunctionApp1.Tests.Mock
{
    public class HttpRequestMock
    {
        public HttpRequest HttpRequest(Dictionary<string, StringValues> query, string body)
        {
            var reqMock = new Mock<HttpRequest>();

            reqMock.Setup(req => req.Query).Returns(new QueryCollection(query));
            var stream = new MemoryStream();

            var writer = new StreamWriter(stream);
            writer.Write(body);
            writer.Flush();
            stream.Position = 0;

            reqMock.Setup(req => req.Body).Returns(stream);
            return reqMock.Object;
        }
    }
}

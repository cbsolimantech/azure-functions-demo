using Microsoft.Extensions.Logging;
using Moq;

namespace FunctionApp1.Tests.Mock
{
    public class LoggerMock
    {
        public ILogger Logger() => new Mock<ILogger>().Object;
    }

}

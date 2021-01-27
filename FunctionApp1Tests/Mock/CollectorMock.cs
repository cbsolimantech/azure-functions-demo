using Microsoft.Azure.WebJobs;
using Moq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FunctionApp1.Tests.Mock
{
    public class CollectorMock<T>
    {
        private List<T> _items = new List<T>();

        public List<T> Items 
        {
            get { return _items; }
        }


        public IAsyncCollector<T> Collector()
        {
            var collector = new Mock<IAsyncCollector<T>>();

            collector.Setup(p => p.AddAsync(It.IsAny<T>(), It.IsAny<CancellationToken>()))
                .Callback((T value, CancellationToken token) => _items.Add(value))
                .Returns(() => Task.FromResult(_items));

            return collector.Object;
        }
    }
}

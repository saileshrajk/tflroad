using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace RoadStatus.Tests.Unit.FakeHandlers
{
    public class HttpHandlerStub: HttpMessageHandler
    {
        private readonly Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> sendAsync;

        public HttpHandlerStub(Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> sendAsync)
        {
            this.sendAsync = sendAsync;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return await sendAsync(request, cancellationToken);
        }
    }
}

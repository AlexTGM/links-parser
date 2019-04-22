using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WebSiteParser.Requests;

namespace WebSiteParser.RequestHandlers
{
    public class WebSiteDownloadRequestHandler : IRequestHandler<WebSiteDownloadRequest, HttpResponseMessage>
    {
        private readonly HttpClient _httpClient;

        public WebSiteDownloadRequestHandler(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient(nameof(WebSiteDownloadRequestHandler));
        }

        public async Task<HttpResponseMessage> Handle(WebSiteDownloadRequest request,
            CancellationToken cancellationToken)
        {
            return await _httpClient.GetAsync(request.Url, cancellationToken);
        }
    }
}
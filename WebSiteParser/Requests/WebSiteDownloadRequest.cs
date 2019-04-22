using System;
using System.Net.Http;
using MediatR;

namespace WebSiteParser.Requests
{
    public class WebSiteDownloadRequest : IRequest<HttpResponseMessage>
    {
        public WebSiteDownloadRequest(Uri url)
        {
            Url = url;
        }

        public Uri Url { get; set; }
    }
}
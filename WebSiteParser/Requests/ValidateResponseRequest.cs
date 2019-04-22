using System.Net.Http;
using MediatR;

namespace WebSiteParser.Requests
{
    public class ValidateResponseRequest : IRequest<bool>
    {
        public ValidateResponseRequest(HttpResponseMessage response, Options options)
        {
            Response = response;
            Options = options;
        }

        public HttpResponseMessage Response { get; set; }
        public Options Options { get; set; }
    }
}
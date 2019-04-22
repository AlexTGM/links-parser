using MediatR;

namespace WebSiteParser.Requests
{
    public class GetParsingOptionsRequest : IRequest<ParsingOptions>
    {
        public GetParsingOptionsRequest(string options)
        {
            Options = options;
        }

        public string Options { get; set; }
    }
}
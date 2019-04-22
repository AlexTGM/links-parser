using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WebSiteParser.Factories;
using WebSiteParser.Parsers.Options;
using WebSiteParser.Parsers.Tags;
using WebSiteParser.Requests;

namespace WebSiteParser.RequestHandlers
{
    public class GetParsingOptionsRequestHandler 
        : IRequestHandler<GetParsingOptionsRequest, ParsingOptions>
    {
        private readonly IOptionsParser _optionsParser;
        private readonly IEnumerable<IParsingOptionsFactory> _parsingOptionsFactories;

        public GetParsingOptionsRequestHandler(IOptionsParser optionsParser,
            IEnumerable<IParsingOptionsFactory> parsingOptionsFactories)
        {
            _optionsParser = optionsParser;
            _parsingOptionsFactories = parsingOptionsFactories;
        }

        public Task<ParsingOptions> Handle(GetParsingOptionsRequest request, CancellationToken cancellationToken)
        {
            var options = _optionsParser.Parse(request.Options);

            var parsingOptions = new ParsingOptions(new List<ITagParser>(), new List<string>(), new List<string>());

            foreach (var option in options)
            {
                var factory = _parsingOptionsFactories.Single(f => f.Name == option.RuleName);

                parsingOptions = factory.Patch(parsingOptions, option.Args.Cast<string>());
            }

            return Task.FromResult(parsingOptions);
        }
    }
}
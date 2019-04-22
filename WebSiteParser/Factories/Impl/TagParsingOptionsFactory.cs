using System.Collections.Generic;
using System.Linq;
using WebSiteParser.Requests;

namespace WebSiteParser.Factories.Impl
{
    public class TagParsingOptionsFactory 
        : IParsingOptionsFactory
    {
        private readonly ITagParserFactory _tagParserFactory;

        public TagParsingOptionsFactory(ITagParserFactory tagParserFactory)
        {
            _tagParserFactory = tagParserFactory;
        }

        public string Name => "tags";

        public ParsingOptions Patch(ParsingOptions options, IEnumerable<string> values)
        {
            options.TagsParsers = values.Select(tag => _tagParserFactory.Create(tag));

            return options;
        }
    }
}
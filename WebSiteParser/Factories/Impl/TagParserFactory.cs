using System.Collections.Generic;
using System.Linq;
using WebSiteParser.Parsers.Tags;

namespace WebSiteParser.Factories.Impl
{
    public class TagParserFactory 
        : ITagParserFactory
    {
        private readonly IEnumerable<ITagParser> _tagsParsers;

        public TagParserFactory(IEnumerable<ITagParser> tagsParsers)
        {
            _tagsParsers = tagsParsers;
        }

        public ITagParser Create(string name)
        {
            return _tagsParsers.Single(parser => parser.TagName == name);
        }
    }
}
using System.Collections.Generic;
using WebSiteParser.Parsers.Tags;

namespace WebSiteParser.Requests
{
    public class ParsingOptions
    {
        public ParsingOptions(IEnumerable<ITagParser> tagsParsers,
            IEnumerable<string> excludeWords,
            IEnumerable<string> includeWords)
        {
            TagsParsers = tagsParsers;
            ExcludeWords = excludeWords;
            IncludeWords = includeWords;
        }

        public IEnumerable<ITagParser> TagsParsers { get; set; }
        public IEnumerable<string> ExcludeWords { get; set; }
        public IEnumerable<string> IncludeWords { get; set; }
    }
}
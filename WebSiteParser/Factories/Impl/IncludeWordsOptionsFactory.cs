using System.Collections.Generic;
using WebSiteParser.Requests;

namespace WebSiteParser.Factories.Impl
{
    public class IncludeWordsOptionsFactory
        : IParsingOptionsFactory
    {
        public string Name => "include";

        public ParsingOptions Patch(ParsingOptions options, IEnumerable<string> values)
        {
            options.IncludeWords = values;

            return options;
        }
    }
}